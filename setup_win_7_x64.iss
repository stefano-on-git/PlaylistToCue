[Setup]
AppName=Playlist To Cue
AppVersion=1.0.1
ArchitecturesInstallIn64BitMode=x64
DefaultDirName={pf}\Playlist To Cue
DefaultGroupName=Playlist To Cue
UninstallDisplayIcon={app}\remove.ico
Compression=lzma2
SolidCompression=yes
OutputDir=setup
OutputBaseFilename=PlaylistToCue-win-7-x64
SetupIconFile=ptc.ico

[Languages]
Name: en; MessagesFile: "compiler:Default.isl"
Name: it; MessagesFile: "compiler:Languages\Italian.isl"   

[Files]
Source: "bin\Debug\netcoreapp1.1\win7-x64\publish\*"; DestDir: "{app}\bin"
Source: "ptc.ico"; DestDir: "{app}"
Source: "ptc.ico"; DestDir: "{app}"

[Icons]
Name: "{app}\Playlist To Cue"; Filename: "{app}\bin\PlaylistToCue.exe"; IconFilename: "{app}\ptc.ico"
Name: "{group}\Playlist To Cue"; Filename: "{app}\bin\PlaylistToCue.exe"; IconFilename: "{app}\ptc.ico"
Name: "{commondesktop}\Playlist To Cue"; Filename: "{app}\bin\PlaylistToCue.exe"; IconFilename: "{app}\ptc.ico"
Name: "{group}\Uninstall Playlist To Cue"; Filename: "{uninstallexe}"; IconFilename: "{app}\remove.ico"

[Registry]
Root: HKCR; Subkey: "directory\shell\Playlist To Cue"; Flags: uninsdeletekey
Root: HKCR; Subkey: "directory\shell\Playlist to Cue"; ValueType: string; ValueName: "Icon"; ValueData: "{app}\ptc.ico"; Flags: uninsdeletekey
Root: HKCR; Subkey: "directory\shell\Playlist to Cue\command"; ValueType: string; ValueData: "{app}\bin\PlaylistToCue.exe ""%1"""; Flags: uninsdeletekey