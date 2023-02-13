using System;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Globalization;

namespace Calculator_MVVM
{
    public abstract class NotifyPropertyBase<TModel> : INotifyPropertyChanged
    {
        /// <summary>
        /// Value indicating the event invoked when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invoked in order to raise the NotifyPropertyChanged event for the specified property.
        /// </summary>
        /// <typeparam name="TResult">Type of the property raising NotifyPropertyChanged.</typeparam>
        /// <param name="property">The property raising NotifyPropertyChanged.</param>
        protected virtual void NotifyPropertyChanged<TResult>(Expression<Func<TModel, TResult>> property)
        {
            MemberExpression member = property.Body as MemberExpression;

            if (member == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Invalid expression '{0}.", property));
            }

            string propertyName = member.Member.Name;

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
