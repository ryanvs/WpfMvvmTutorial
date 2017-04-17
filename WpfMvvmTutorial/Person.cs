using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMvvmTutorial
{
    public class Person : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Person()
        {
            // Trigger validation
            _validationTemplate = new ValidationTemplate(this);
        }

        private string firstName;

        [Required]
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FirstName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FullName"));
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FullName"));
            }
        }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        #region Validation
        private ValidationTemplate _validationTemplate;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add { _validationTemplate.ErrorsChanged += value; }
            remove { _validationTemplate.ErrorsChanged -= value; }
        }

        public string Error
        {
            get { return _validationTemplate.Error; }
        }

        public bool HasErrors
        {
            get { return _validationTemplate.HasErrors; }
        }

        public string this[string columnName]
        {
            get { return _validationTemplate[columnName]; }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _validationTemplate.GetErrors(propertyName);
        }

        #endregion
    }
}
