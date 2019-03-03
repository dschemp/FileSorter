# FileSorter

This program utilizes the [CommandLineParser](https://github.com/commandlineparser/commandline) library to make it user-friendly.

### Arguments:

| Argument                  	| Required 	| Default 	| Help Text                                                                       	|
|---------------------------	|----------	|---------	|---------------------------------------------------------------------------------	|
| -p / --path (path)        	| yes      	|         	| Set the path to the directory to be sorted.                                     	|
| -d / --debug              	| no       	| false   	| Shows you verbose messages.                                                     	|
| --move                    	| no       	| false   	| Move all files instead of copying them into the directory.                      	|
| -l / --maxlength (length) 	| no       	| 16      	| Sets the length of the original filename to be prepended onto the new filename. 	|
| -o / --override           	| no       	| false   	| Override files that already exists when copying / moving.                       	|
| -k / --keep               	| no       	| 14      	| Sets the days a file has to be accessed past to be copied / moved.              	|
| --help                    	|          	|         	| Display the help screen.                                                        	|
| --version                 	|          	|         	| Show the program's version.                                                     	|

