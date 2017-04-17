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
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

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

        public bool HasErrors
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
