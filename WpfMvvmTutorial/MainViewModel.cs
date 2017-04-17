using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace WpfMvvmTutorial
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            EditingName = new Person();
            nameCollection = new ObservableCollection<Person>();
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            SubmitCommand = new RelayCommand(Submit);

            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                EditingName.FirstName = "John";
                EditingName.LastName = "Doe";
                nameCollection.Add(new Person { FirstName = "Jane", LastName = "Doe" });
                nameCollection.Add(new Person { FirstName = "Sponge", LastName = "Bob" });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Person EditingName { get; private set; }

        public Person SelectedName { get; set; }

        public RelayCommand SubmitCommand
        {   get;
            private set;
        }

        private void Submit(object _)
        {
            // NOTE: Be careful if you add a reference to the collection. You must either copy the object
            // to a new object, or create a new object for EditingName

            // Option 1: create a copy (this should be refactor to IClonable or another mechanism rather
            // than manual properties)
            var copyName = new Person { FirstName = EditingName.FirstName, LastName = EditingName.LastName };
            nameCollection.Add(copyName);
            MessageBox.Show(string.Format("Hello {0}", copyName.FullName));
            EditingName.FirstName = "";
            EditingName.LastName = "";

            // Option 2: create a new object for the editing process
            //EditingName = new Person();
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EditingName"));
        }

        public RelayCommand DeleteCommand
        {
            get;
            private set;
        }

        private bool CanDelete(object _)
        {
            return SelectedName != null;
        }

        private void Delete(object _)
        {
            var item = SelectedName;
            if (item != null)
            {
                NameCollection.Remove(item);
            }
        }

        private ObservableCollection<Person> nameCollection;

        public ObservableCollection<Person> NameCollection
        {
            get { return nameCollection; }
        }
    }
}
