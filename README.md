# AdvancedConsole
## Advanced console features for .NET
<img src="https://raw.githubusercontent.com/dotnettools/AdvancedConsole/main/assets/icon.png" alt="AdvancedConsole" width="64"/>

### Features
- Colorize the content
- Use out-of-the-box widgets (including progress bar, select list etc.) and controllers or write your own
- No flickers or any unpleasant visual effect.
- Highly customizable

### Installation
Install the `DotNetTools.AdvancedConsole` .NET Standard NuGet package.

    Install-Package DotNetTools.AdvancedConsole -Version 0.0.4
    
### Examples
#### Displaying a menu

	class Program
	{
		static void Main(string[] args)
		{
			var program = new ConsoleProgram();
			program.Controller = new ConsoleMenuController
			{
				FixedWidth = 50,
				HeaderText = "Please select an option.",
				Items = new List<ConsoleMenuItem>
				{
					new ConsoleMenuItem { Text = "Item 1" },
					new ConsoleMenuItem { Text = "Item 2" },
					new ConsoleMenuItem { Text = "Exit" }.Select(()=> program.Terminate()),
				}
			};
			program.Run();
		}
	}
