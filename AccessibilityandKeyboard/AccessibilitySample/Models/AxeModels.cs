using System.Text.Json.Serialization;

namespace AccessibilitySample.Models;

public class AxeResults
{
    [JsonPropertyName("violations")]
    public List<AxeViolation> Violations { get; set; } = new();

    [JsonPropertyName("incomplete")]
    public List<AxeViolation> Incomplete { get; set; } = new();
}

public class AxeViolation
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("help")]
    public string Help { get; set; } = string.Empty;

    [JsonPropertyName("helpUrl")]
    public string HelpUrl { get; set; } = string.Empty;

    [JsonPropertyName("impact")]
    public string? Impact { get; set; }

    [JsonPropertyName("nodes")]
    public List<AxeNode> Nodes { get; set; } = new();
}

public class AxeNode
{
    [JsonPropertyName("target")]
    public List<string> Target { get; set; } = new();

    [JsonPropertyName("html")]
    public string Html { get; set; } = string.Empty;
}

public class ShortcutGroup
{
    public string Category { get; set; } = string.Empty;
    public List<ShortcutItem> Shortcuts { get; set; } = new();
}

public class ShortcutItem
{
    public List<string> Keys { get; set; } = new();
    public string Description { get; set; } = string.Empty;
}

public static class KeyboardShortcuts
{
    public static readonly List<ShortcutGroup> All = new()
    {
        new ShortcutGroup
        {
            Category = "Navigation",
            Shortcuts = new()
            {
                new() { Keys = new() { "Tab" }, Description = "Move to the next active element in the grid" },
                new() { Keys = new() { "Shift", "Tab" }, Description = "Move to the previous active element" },
                new() { Keys = new() { "Up arrow" }, Description = "Move focus to the cell above" },
                new() { Keys = new() { "Down arrow" }, Description = "Move focus to the cell below" },
                new() { Keys = new() { "Left arrow" }, Description = "Move focus to the left cell" },
                new() { Keys = new() { "Right arrow" }, Description = "Move focus to the right cell" },
                new() { Keys = new() { "Home" }, Description = "Move focus to the first cell of the current row" },
                new() { Keys = new() { "End" }, Description = "Move focus to the last cell of the current row" },
                new() { Keys = new() { "Ctrl or Cmd", "Home" }, Description = "Move focus to the first cell of the first row" },
                new() { Keys = new() { "Ctrl or Cmd", "End" }, Description = "Move focus to the last cell of the last row" },
                new() { Keys = new() { "Alt", "J" }, Description = "Moves the focus to the entire grid" },
                new() { Keys = new() { "Alt", "W" }, Description = "Focus the grid content" },
            }
        },
        new ShortcutGroup
        {
            Category = "Header (Sorting)",
            Shortcuts = new()
            {
                new() { Keys = new() { "Enter" }, Description = "Sort the focused column" },
                new() { Keys = new() { "Ctrl or Cmd", "Enter" }, Description = "Multi-column sort on the focused header" },
                new() { Keys = new() { "Shift", "Enter" }, Description = "Clear sorting on the focused column" },
            }
        },
        new ShortcutGroup
        {
            Category = "Selection",
            Shortcuts = new()
            {
                new() { Keys = new() { "Up arrow" }, Description = "Move row/cell selection upward" },
                new() { Keys = new() { "Down arrow" }, Description = "Move row/cell selection downward" },
                new() { Keys = new() { "Left arrow" }, Description = "Move cell selection to the left" },
                new() { Keys = new() { "Right arrow" }, Description = "Move cell selection to the right" },
                new() { Keys = new() { "Shift", "Up arrow" }, Description = "Extend row selection upward" },
                new() { Keys = new() { "Shift", "Down arrow" }, Description = "Extend row selection downward" },
                new() { Keys = new() { "Shift", "Left arrow" }, Description = "Extend cell selection to the left" },
                new() { Keys = new() { "Shift", "Right arrow" }, Description = "Extend cell selection to the right" },
                new() { Keys = new() { "Enter" }, Description = "Move selection to the next row/cell" },
                new() { Keys = new() { "Shift", "Enter" }, Description = "Move selection to the previous row/cell" },
                new() { Keys = new() { "Esc" }, Description = "Clear all selections" },
                new() { Keys = new() { "Ctrl or Cmd", "A" }, Description = "Select all rows or cells" },
            }
        },
        new ShortcutGroup
        {
            Category = "Grouping",
            Shortcuts = new()
            {
                new() { Keys = new() { "Ctrl or Cmd", "Down Arrow" }, Description = "Expand all visible group rows" },
                new() { Keys = new() { "Ctrl or Cmd", "Up Arrow" }, Description = "Collapse all visible group rows" },
                new() { Keys = new() { "Ctrl or Cmd", "Space" }, Description = "Group by the focused column header" },
            }
        },
        new ShortcutGroup
        {
            Category = "Pager",
            Shortcuts = new()
            {
                new() { Keys = new() { "Page up" }, Description = "Navigate to the previous page" },
                new() { Keys = new() { "Page down" }, Description = "Navigate to the next page" },
                new() { Keys = new() { "Enter" }, Description = "Select the focused page" },
                new() { Keys = new() { "Tab" }, Description = "Move focus to the next pager item" },
                new() { Keys = new() { "Shift", "Tab" }, Description = "Move focus to the previous pager item" },
                new() { Keys = new() { "Home" }, Description = "Navigate to the first page" },
                new() { Keys = new() { "End" }, Description = "Navigate to the last page" },
            }
        },
        new ShortcutGroup
        {
            Category = "Editing",
            Shortcuts = new()
            {
                new() { Keys = new() { "F2" }, Description = "Enter edit mode if editing is enabled" },
                new() { Keys = new() { "Enter" }, Description = "Save changes and exit edit mode" },
                new() { Keys = new() { "Escape" }, Description = "Cancel editing and revert changes" },
                new() { Keys = new() { "Tab" }, Description = "Move to the next editable cell" },
                new() { Keys = new() { "Shift", "Tab" }, Description = "Move to the previous editable cell" },
            }
        }
    };
}