using System.ComponentModel;
using System.Reactive.Disposables;
using PROWeb.Common.StrongBindings.Converters;
using PROWeb.Common.StrongBindings.Enums;

namespace PROWeb.Common.StrongBindings;

/// <summary>
/// Binding instance implementation.
/// </summary>
public class StrongInstanceBinding<TSource, TTarget, TSourceProperty, TTargetProperty> : IStrongInstanceBinding<TSource, TTarget,
    TSourceProperty, TTargetProperty>
{
    private bool _isSuspended = true;

    public StrongInstanceBinding(
        TSource source,
        TTarget target,
        StrongBindingPath<TSource, TSourceProperty> sourcePath,
        StrongBindingPath<TTarget, TTargetProperty> targetPath,
        StrongBindingMode mode,
        IStrongValueConverter<TSourceProperty, TTargetProperty>? converter)
    {
        Source = source;
        Target = target;
        SourcePath = sourcePath;
        TargetPath = targetPath;
        Mode = mode;
        Converter = converter;

        // Initial sync.
        Resume();
    }

    public TSource Source { get; }

    public TTarget Target { get; }

    public StrongBindingMode Mode { get; }
    public IStrongValueConverter<TSourceProperty, TTargetProperty>? Converter { get; }

    public StrongBindingPath<TSource, TSourceProperty> SourcePath { get; }

    public StrongBindingPath<TTarget, TTargetProperty> TargetPath { get; }

    protected bool IsTargetUpdateSuspended { get; set; }

    protected bool IsSourceUpdateSuspended { get; set; }

    public void Sync()
    {
        if (Mode == StrongBindingMode.OneWayToSource)
        {
            UpdateSource();
        }
        else
        {
            UpdateTarget();
        }
    }

    public virtual void Dispose()
    {
        StopTracking();
    }

    public void Suspend()
    {
        if (!_isSuspended)
        {
            StopTracking();
        }
    }

    public void Resume()
    {
        if (_isSuspended)
        {
            Sync();
            StartTracking();
        }
    }

    protected virtual void StartSourceTracking()
    {
        if (Source is INotifyPropertyChanged nSource)
        {
            nSource.PropertyChanged += OnSourcePropertyChanged;
        }
    }

    protected virtual void StartTargetTracking()
    {
        if (Target is INotifyPropertyChanged nTarget)
        {
            nTarget.PropertyChanged += OnTargetPropertyChanged;
        }
    }

    protected virtual void StopSourceTracking()
    {
        if (Source is INotifyPropertyChanged nSource)
        {
            nSource.PropertyChanged -= OnSourcePropertyChanged;
        }
    }

    protected virtual void StopTargetTracking()
    {
        if (Target is INotifyPropertyChanged nTarget)
        {
            nTarget.PropertyChanged -= OnTargetPropertyChanged;
        }
    }

    protected virtual void OnTargetPropertyChanged()
    {
        if ((Mode == StrongBindingMode.TwoWay || Mode == StrongBindingMode.OneWayToSource) && !IsSourceUpdateSuspended)
        {
            using (SuspendTargetUpdate())
            {
                UpdateSource();
            }
        }
    }

    protected virtual void OnSourcePropertyChanged()
    {
        if (Mode != StrongBindingMode.OneWayToSource && !IsTargetUpdateSuspended)
        {
            using (SuspendSourceUpdate())
            {
                UpdateTarget();
            }
        }
    }

    private void StartTracking()
    {
        if (Mode == StrongBindingMode.OneTime)
        {
            return;
        }

        if (Mode == StrongBindingMode.TwoWay || Mode == StrongBindingMode.OneWayToSource)
        {
            StartTargetTracking();
        }

        if (Mode != StrongBindingMode.OneWayToSource)
        {
            StartSourceTracking();
        }

        _isSuspended = false;
    }

    private void StopTracking()
    {
        StopSourceTracking();
        StopTargetTracking();

        _isSuspended = true;
    }

    private void OnSourcePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (SourcePath.PropertyName == e.PropertyName)
        {
            OnSourcePropertyChanged();
        }
    }

    private void OnTargetPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (TargetPath.PropertyName == e.PropertyName)
        {
            OnTargetPropertyChanged();
        }
    }

    private IDisposable SuspendSourceUpdate()
    {
        IsSourceUpdateSuspended = true;

        return Disposable.Create(
            this,
            instance =>
            {
                instance.IsSourceUpdateSuspended = false;
            });
    }

    private void UpdateSource()
    {
        TTargetProperty? tValue = TargetPath.ReadProperty(Target);
        TSourceProperty? sValue = SourcePath.ReadProperty(Source);
        TSourceProperty? tsValue = default;

        if (Converter != null)
        {
            tsValue = Converter.ToSourceProperty(tValue);
        }
        else if (tValue is TSourceProperty value)
        {
            tsValue = value;
        }

        //Do not update source if value of source is already same as target value. 
        if (EqualityComparer<TSourceProperty>.Default.Equals(tsValue, sValue))
        {
            return;
        }

        SourcePath.WriteProperty(Source, tsValue);
    }

    private void UpdateTarget()
    {
        TSourceProperty? sValue = SourcePath.ReadProperty(Source);
        TTargetProperty? tValue = TargetPath.ReadProperty(Target);
        TTargetProperty? stValue = default;

        if (Converter != null)
        {
            stValue = Converter.ToTargetProperty(sValue);
        }
        else if (sValue is TTargetProperty value)
        {
            stValue = value;
        }

        //Do not update target if value of target is already same as source value. 
        if (EqualityComparer<TTargetProperty>.Default.Equals(stValue, tValue))
        {
            return;
        }

        TargetPath.WriteProperty(Target, stValue);
    }

    private IDisposable SuspendTargetUpdate()
    {
        IsTargetUpdateSuspended = true;

        return Disposable.Create(
            this,
            instance =>
            {
                instance.IsTargetUpdateSuspended = false;
            });
    }
}
