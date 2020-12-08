# C# Heists
>Text-based heist adventure game for the terminal

![C# Heists overview GIF](/readme_overview.gif)

## Features
- manage a crew of untrustworthy criminals
    - hire associates
    - ice unruly associates
    - manage morale (unhappy associates may turn against you!)
    - each associate has a unique ASCII face
- perform five heists throughout Nashville
    - each with ASCII art
- six endings based on how much money you got away with, whether you survived, or got arrested
- enough randomization so no two playthroughs have the same outcome

## Setup
### Windows
- Download ```win64_csharp-heists-v1.0.zip``` from: https://github.com/ste163/csharp_heists/releases/tag/v1.0
- Run ```heist.exe```.

### Linux
- Download ```lin64_csharp-heists-v1.0.zip``` from: https://github.com/ste163/csharp_heists/releases/tag/v1.0
- Change permissions on CSharpHeists file with ```chmod 777 ./CSharpHeists```
- Run with ```./CSharpHeists```

### Mac
#### Prerequisites
- .NET 5
#### Download the project
- ```git clone``` repo and ```cd``` into it.
```
git clone git@github.com:ste163/csharp_heists.git
cd csharp_heists
```
#### Build & run the application
- Inside the ```csharp_heists``` directory, run ```dotnet run``` to build and run the application.

## ASCII Art Credits
Thanks to these sites for the amazing ASCII artwork:
- https://textfancy.com/multiline-text-art/
- https://asciiart.website/
- https://asciiart.website/index.php?art=people/faces
- https://simpletextart.blogspot.com/2013/12/money-bag-picture-made-with-ascii-text.html
- https://www.asciiart.eu/buildings-and-places/cities
- http://www.asciiworld.com/-Buildings-.html

## License
[MIT](/LICENSE)