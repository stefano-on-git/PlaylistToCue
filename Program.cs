using System;
using System.Text.RegularExpressions;

namespace PlaylistToCue
{
    public class Program
    {        
        public static void Main(string[] args)
        {
            
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            string basePath = null;
            string playlistPath = null;

            var culturName = System.Globalization.CultureInfo.CurrentCulture.EnglishName;

            Console.Title = "Playlist To Cue";   

            if (args.Length > 0) {

                basePath = args[0];
                Console.WriteLine("");

            } else {

                if (culturName.ToLower().Contains("italian")) {
                    
                    Console.WriteLine("Inserire il percorso della directory contenente i files:");

                } else {
                    
                    Console.WriteLine("Enter directory path which contains the files:");

                }
                
                Console.WriteLine("");
            
                basePath = Console.ReadLine();

            }

            if (String.IsNullOrEmpty(basePath) || !System.IO.Directory.Exists(basePath)) {

                
                if (culturName.ToLower().Contains("italian")) {
                    
                    Console.WriteLine("ERRORE: Non è stato inserito nessun percorso valido");

                } else {
                    
                    Console.WriteLine("ERROR: No valid path has been entered");

                }

            } else {

                string[] elencofiles = System.IO.Directory.GetFiles(basePath);
            
                string fileMix = null;
                string titleMix = null;
                string performerMix = null;

                if (elencofiles.Length > 0) {
                    
                    foreach(string file in elencofiles) {
                        
                        if (System.IO.Path.GetExtension(file) == ".mp3") {
                            
                            try
                            {
                                
                                fileMix = System.IO.Path.GetFileName(file);
                                titleMix = fileMix.Split('-')[1].Trim().Replace(".mp3","");
                                performerMix = fileMix.Split('-')[0].Trim(); 

                            }
                            catch { }
                        
                            break;
                                                
                        }
                        
                    }

                    if (string.IsNullOrEmpty(titleMix)) {
                        
                        if (culturName.ToLower().Contains("italian")) {
                            
                            Console.WriteLine("ERRORE: Nel percorso indicato non è presente nessun file mp3 o il formato del nome file non è valido");

                        } else {
                            
                            Console.WriteLine("ERROR: In the provided path is not present any mp3 file or the filename format is not valid");

                        }

                    } else {

                        string outputCueFile = null;
                        
                        outputCueFile += "TITLE " + "\"" + titleMix + "\"\r\n";
                        outputCueFile += "PERFORMER " + "\"" + performerMix + "\"\r\n";
                        outputCueFile += "FILE " + "\"" + fileMix + "\" MP3\r\n";
                        
                        playlistPath = basePath + @"\playlist.txt";

                        if (System.IO.File.Exists(playlistPath)) {

                            string[] playlist = System.IO.File.ReadAllLines(playlistPath);
                            
                            int i = 0;
                            
                            Regex regexTrackIndex = new Regex("[0-9]+:[0-9]+");

                            if (playlist.Length > 0) {

                                bool canCreate = false;

                                string trackInfo = null;

                                foreach (string track in playlist)
                                {

                                    if (!string.IsNullOrEmpty(track)) {

                                        i++;

                                        try
                                        {
                                            
                                            trackInfo = "  TRACK " + (i < 10 ? "0" + i.ToString() : i.ToString()) + " AUDIO\r\n";
                                            trackInfo += "    TITLE " + "\"" + regexTrackIndex.Replace(Regex.Split(track, " - ")[1], "").Trim()  + "\"\r\n";
                                            trackInfo += "    PERFORMER " + "\"" + Regex.Split(track, " - ")[0].Trim()  + "\"\r\n";
                                            
                                            string trackIndex = regexTrackIndex.Match(Regex.Split(track, " - ")[1].Trim()).Value;

                                            if (i > 1 && string.IsNullOrEmpty(trackIndex)) {
                                                
                                                if (culturName.ToLower().Contains("italian")) {
                                                    
                                                    Console.WriteLine("ERRORE: Il file playlist.txt non rispetta il formato previsto");

                                                } else {
                                                    
                                                    Console.WriteLine("ERROR: The playlist.txt file doesn't meet the required format");

                                                }

                                                EsciDalProgramma(culturName.ToLower());

                                                return;

                                            }

                                            outputCueFile += trackInfo;
                                            
                                            outputCueFile += "    INDEX 01 " + (trackIndex == "" ? "00:00:00" : trackIndex + ":00") + "\r\n";

                                            if (!canCreate) canCreate = true;

                                        }
                                        catch {

                                            i--;

                                            /*
                                            if (culturName.ToLower().Contains("italian")) {
                                                
                                                Console.WriteLine("ERRORE: Il file playlist.txt non rispetta il formato previsto");

                                            } else {
                                                
                                                Console.WriteLine("ERROR: The playlist.txt file doesn't meet the required format");

                                            }                                            
                                            
                                            EsciDalProgramma();

                                            return;
                                            */

                                        }
                                    }
                                    
                                }

                                if (canCreate) {
                                    
                                    try
                                    {
                                        
                                        System.IO.File.WriteAllText(basePath + @"\" + fileMix.Replace(".mp3", ".cue"), outputCueFile);
                                    
                                        if (culturName.ToLower().Contains("italian")) {
                                            
                                            Console.WriteLine("File: " + fileMix.Replace(".mp3", ".cue") + " creato correttamente");

                                        } else {
                                            
                                            Console.WriteLine("File: " + fileMix.Replace(".mp3", ".cue") + " successfully created");

                                        }   

                                    }
                                    catch
                                    {
                                        
                                        if (culturName.ToLower().Contains("italian")) {
                                            
                                            Console.WriteLine("ERRORE: Creazione del file fallita, riprovare");

                                        } else {
                                            
                                            Console.WriteLine("ERROR: Failed to create file, try again");

                                        }
                                        
                                    }


                                } else {

                                    if (culturName.ToLower().Contains("italian")) {
                                        
                                        Console.WriteLine("ERRORE: Il file playlist.txt non rispetta il formato previsto");

                                    } else {
                                        
                                        Console.WriteLine("ERROR: The playlist.txt file doesn't meet the required format");

                                    }

                                }                                

                            } else {

                                if (culturName.ToLower().Contains("italian")) {
                                    
                                    Console.WriteLine("ERRORE: Il file playlist.txt è vuoto");

                                } else {
                                    
                                    Console.WriteLine("ERROR: The playlist.txt file is empty");

                                }

                            }                            

                        } else {

                            if (culturName.ToLower().Contains("italian")) {
                                
                                Console.WriteLine("ERRORE: Nel percorso indicato non è presente il file playlist.txt");

                            } else {
                                
                                Console.WriteLine("ERRORE: In the provided path is not present the playlist.txt file");

                            }

                        }                        

                    }

                } 

                else {

                    if (culturName.ToLower().Contains("italian")) {
                        
                        Console.WriteLine("ERRORE: Nel percorso indicato non è presente nessun file");

                    } else {
                        
                        Console.WriteLine("ERROR: In the provided path there isn't any file");

                    }

                }

            } 
            
            EsciDalProgramma(culturName.ToLower());

        }

        public static void EsciDalProgramma(string culture) {

            Console.WriteLine("");

            if (culture.Contains("italian")) {

                Console.WriteLine("Premere un tasto per uscire.");  

            }
            else {

                Console.WriteLine("Press any key to exit."); 

            }

            Console.ReadKey();

        }

    }

}