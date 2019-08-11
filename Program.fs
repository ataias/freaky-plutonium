// Learn more about F# at http://fsharp.org

open System
open System.Diagnostics

let make_lynx_args (base_link:string) : string =
    [ "-display_charset=UTF-8";
        "-dump";
        base_link
      ] |> List.fold (fun x y -> x + " " + y) ""

let download_page (link:string) : string =
    let prog_and_args = make_lynx_args link

    let proc = new Process(
                StartInfo = ProcessStartInfo(
                    FileName = "lynx",
                    Arguments = prog_and_args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                ))

    proc.Start() |> ignore
    proc.StandardOutput.ReadToEnd()

let download_reverso (word:string) : string =
    let reverso_base = "https://context.reverso.net/translation/french-english/{0}"
    let link = String.Format(reverso_base, word)
    download_page link

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    printfn "%s" (download_reverso "aveuglement")
    0 // return an integer exit code
