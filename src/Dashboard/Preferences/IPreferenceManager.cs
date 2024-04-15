using MudBlazor;

namespace Dashboard.Preferences;

public interface IPreferenceManager
{
    Task<MudTheme> GetCurrentThemeAsync();
    Task<bool> ToggleDarkModeAsync();
    Task<bool> ChangeDrawerStateAsync();
    Task<bool> IsDrawerOpenAsync();
}