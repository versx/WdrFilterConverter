# WDR Filter Converter  

Convert [WDR](https://github.com/PartTimeJS/WDR) channel filters to [WhMgr](https://github.com/versx/WhMgr) compatible filters.  

1. Download and install .NET 2.1  
1. Clone repository `git clone https://github.com/versx/WdrFilterConverter`  
1. Build project `~/.dotnet/dotnet build` in root project folder  
1. Copy `pokemon.json` to `bin/pokemon.json`  
1. Copy WDR filters to `bin/wdr_filters` folder (Create if it doesn't exist)  
1. Run `~/.dotnet/dotnet WdrFilterConverter.dll`  
1. Converted filters will be saved to the `bin/filters` folder named respectively  
