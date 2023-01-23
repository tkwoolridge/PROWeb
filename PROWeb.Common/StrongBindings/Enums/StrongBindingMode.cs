namespace PROWeb.Common.StrongBindings.Enums;

/// <summary>
/// Strong binding modes.
/// </summary>
public enum StrongBindingMode
{
    /// <summary>Binds one way from source to target.</summary>
    OneWay,

    /// <summary>
    /// Binds two-way with the initial value coming from the target.
    /// </summary>
    TwoWay,

    /// <summary>
    /// Updates the target when the application starts or when the data context changes.
    /// </summary>
    OneTime,

    /// <summary>Binds one way from target to source.</summary>
    OneWayToSource,
}
