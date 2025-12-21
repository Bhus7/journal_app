using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalApp.Services
{
    public class ThemeState
    {
        public bool IsDark { get; private set; }

        public event Action? OnChange;

        public void Toggle()
        {
            IsDark = !IsDark;
            OnChange?.Invoke();
        }
    }
}
