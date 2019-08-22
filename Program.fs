// Learn more about F# at http://fsharp.org

open System
open System.Diagnostics

/// Runs programs and gets standard output in variable
let runCmd (cmd:string) (args:string) : string =
    let proc = new Process(
                StartInfo = ProcessStartInfo(
                    FileName = cmd,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                ))

    proc.Start() |> ignore
    proc.StandardOutput.ReadToEnd()

let makeLynxArgs (baseLink:string) : string =
    [ "-display_charset=UTF-8";
        "-dump";
        baseLink
      ] |> List.fold (fun x y -> x + " " + y) ""

/// Asdf asdf
/// Asdf asdf 2
let downloadPage (link:string) : string =
    let args = makeLynxArgs link
    runCmd "lynx" args

let downloadReverso (word:string) : string =
    let reversoBase = "https://context.reverso.net/translation/french-english/{0}"
    let link = String.Format(reversoBase, word)
    downloadPage link

exception UnexpectedDataFormat of string

let trimReverso (data:string) : string option =
    let separator = "Translation of"
    let parts = data.Split separator |> Seq.toList
    let data =
        match parts with
          | hd::tl::[] -> Some (separator + hd)
          | _ -> None
    data

[<EntryPoint>]
let main argv =
    printfn "%s" (downloadReverso "aveuglement")
    0 // return an integer exit code
