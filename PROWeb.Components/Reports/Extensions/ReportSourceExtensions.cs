using PROWeb.Components.Reports.Models;
using PROWeb.Data.Models.Enums;
using PROWeb.Data.Models.Reports;
using System.Data;
using Telerik.Reporting;

namespace PROWeb.Components.Reports.Extensions
{
    public static class ReportSourceExtensions
    {
        public static void SetDataSource(this SqlDataSource dataSource, ReportDescriptor report)
        {
            IDictionary<string, int> paremeters = new Dictionary<string, int>();

            string ToCondition(ReportFilter filter, string parameter)
            {
                return filter.Operator switch
                {
                    ReportFilterOperator.IsEqualTo => $"{filter.Name} = {parameter}",
                    ReportFilterOperator.IsNotEqualTo => $"{filter.Name} <> {parameter}",
                    ReportFilterOperator.IsGreaterThanOrEqualTo => $"{filter.Name} >= {parameter}",
                    ReportFilterOperator.IsLessThanOrEqualTo => $"{filter.Name} <= {parameter}",
                    ReportFilterOperator.IsLessThan => $"{filter.Name} < {parameter}",
                    ReportFilterOperator.IsGreaterThan => $"{filter.Name} > {parameter}",
                    ReportFilterOperator.DoesNotContain => $"{filter.Name} NOT IN ({parameter})",
                    ReportFilterOperator.IsContainedIn => $"{filter.Name} IN ({parameter})",
                    ReportFilterOperator.Contains => $"{filter.Name} LIKE {parameter}",
                    ReportFilterOperator.EndsWith => $"{filter.Name} LIKE {parameter}",
                    ReportFilterOperator.StartsWith => $"{filter.Name} LIKE {parameter}",
                    ReportFilterOperator.IsNull => $"{filter.Name} IS NULL",
                    ReportFilterOperator.IsNotNull => $"{filter.Name} IS NOT NULL",
                    ReportFilterOperator.IsNullOrEmpty => $"({filter.Name} IS NULL OR LEN({filter.Name}) = 0)",
                    ReportFilterOperator.IsNotNullOrEmpty => $"({filter.Name} IS NOT NULL OR LEN({filter.Name}) > 0)",
                    ReportFilterOperator.IsEmpty => $"LENGTH({filter.Name}) = 0",
                    ReportFilterOperator.IsNotEmpty => $"LEN({filter.Name}) > 0",
                    _ => throw new NotImplementedException()
                };
            }

            string GetParameter(ReportFilter filter)
            {
                int count;

                if (filter.Name is { } name && paremeters.TryGetValue(name, out count))
                {
                    count++;
                    paremeters[name] = count;
                }
                else
                {
                    count = 1;
                    paremeters.Add(filter.Name!, count);
                }

                return "@" + filter.Name + count;
            }

            void AddParameter(ReportFilter filter, string parameter)
            {
                bool ignore = filter.Operator switch
                {
                    ReportFilterOperator.IsNull or
                    ReportFilterOperator.IsNotNull or
                    ReportFilterOperator.IsNullOrEmpty or
                    ReportFilterOperator.IsNotNullOrEmpty or
                    ReportFilterOperator.IsEmpty or
                    ReportFilterOperator.IsNotEmpty => true,
                    _ => false
                };

                if (ignore)
                {
                    return;
                }

                object? value = filter.Value ?? DBNull.Value;

                if (filter.FilterType == typeof(string))
                {
                    object? sValue = filter.Operator switch
                    {
                        ReportFilterOperator.Contains => $"%{value}%",
                        ReportFilterOperator.EndsWith => $"%{value}",
                        ReportFilterOperator.StartsWith => $"{value}%",
                        _ => filter.Value?.ToString()
                    };

                    dataSource.Parameters.Add(parameter, DbType.String, sValue ?? value);
                }
                else if (filter.FilterType == typeof(bool))
                {
                    dataSource.Parameters.Add(parameter, DbType.Boolean, value);
                }
                else if (filter.FilterType == typeof(int))
                {
                    dataSource.Parameters.Add(parameter, DbType.Int32, value);
                }
                else if (filter.FilterType == typeof(DateTime))
                {
                    dataSource.Parameters.Add(parameter, DbType.DateTime, value);
                }
                else
                {
                    SqlDataSourceParameter sqlParameter = new SqlDataSourceParameter()
                    {
                        Name = filter.Name,
                        Value = value
                    };

                    dataSource.Parameters.Add(sqlParameter);
                }
            }

            string ToWhere(ReportFilterGroup root)
            {
                Queue<ReportFilterGroup> queue = new Queue<ReportFilterGroup>();

                queue.Enqueue(root);

                string where = string.Empty;

                while (queue.Count > 0)
                {
                    ReportFilterGroup group = queue.Dequeue();

                    var filters = group.Filters;

                    where += where == string.Empty ? "(" : group.LogicalOperator switch
                    {
                        ReportFilterLogicalOperator.Or => " OR (",
                        _ => " AND ("
                    };

                    foreach (var child in filters)
                    {
                        switch (child)
                        {
                            case ReportFilter filter:
                                {
                                    string parameter = GetParameter(filter);

                                    where += (filter != filters[0] ? " AND " : string.Empty) + ToCondition(filter, parameter);
                                    AddParameter(filter, parameter);

                                    break;
                                }
                            case ReportFilterGroup filterGroup:
                                {
                                    queue.Enqueue(filterGroup);
                                    break;
                                }
                        }
                    }

                    where += ")";
                }

                return where;
            }

            string condition = ToWhere(report.Filters);

            dataSource.SelectCommand = $"{dataSource.SelectCommand} WHERE {condition}";
            dataSource.ConnectionString = report.Connection;
        }
    }
}
