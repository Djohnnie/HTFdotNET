using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace HTF.Mars.StreamSource
{
    public class ObservableBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies a property change.
        /// </summary>
        /// <param name="propertySelector">Expression to a property that has changed.</param>
        public void Notify<TModel, TValue>(Expression<Func<TModel, TValue>> propertySelector)
        {
            var memberExpression = propertySelector?.Body as MemberExpression;
            if (memberExpression != null)
            {
                RaisePropertyChanged(new PropertyChangedEventArgs(memberExpression.Member.Name));
            }
        }

        public void Update<TModel, TValue>(Expression<Func<TModel, TValue>> propertySelector, Action setNewValue, TValue currentValue, TValue newValue)
        {
            // Remeber difference
            var memberExpression = propertySelector?.Body as MemberExpression;
            if (memberExpression != null)
            {
                // Enkel indien anders
                if (!Equals(currentValue, newValue))
                {
                    setNewValue();
                    // Pass the message
                    this.Notify(propertySelector);
                }
            }
        }

        public virtual void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
    }
    public static class ObservableBaseExtension
    {

        public static void NotifyPropertyChanged<TModel, TValue>(this TModel observableBase, Expression<Func<TModel, TValue>> propertySelector) where TModel : ObservableBase
        {
            observableBase.Notify(propertySelector);
        }

        public static void NotifyPropertyChanged<TModel, TValue1, TValue2>(this TModel observableBase, Expression<Func<TModel, TValue1>> propertySelector1, Expression<Func<TModel, TValue2>> propertySelector2) where TModel : ObservableBase
        {
            observableBase.NotifyPropertyChanged(propertySelector1);
            observableBase.NotifyPropertyChanged(propertySelector2);
        }

        public static void Update<TModel, TValue>(this TModel observableBase, Expression<Func<TModel, TValue>> propertySelector, Action setNewValue, TValue currentValue, TValue newValue) where TModel : ObservableBase
        {
            observableBase.Update(propertySelector, setNewValue, currentValue, newValue);
        }
    }
}