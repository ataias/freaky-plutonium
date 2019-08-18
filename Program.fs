// Learn more about F# at http://fsharp.org

open System
open System.Diagnostics

/// Runs programs and gets standard output in variable
let run_cmd (cmd:string) (args:string) : string =
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

let make_lynx_args (base_link:string) : string =
    [ "-display_charset=UTF-8";
        "-dump";
        base_link
      ] |> List.fold (fun x y -> x + " " + y) ""

let download_page (link:string) : string =
    let args = make_lynx_args link
    run_cmd "lynx" args

let download_reverso (word:string) : string =
    let reverso_base = "https://context.reverso.net/translation/french-english/{0}"
    let link = String.Format(reverso_base, word)
    download_page link

exception UnexpectedDataFormat of string

let trim_reverso (data:string) : string option =
    let separator = "Translation of"
    let parts = data.Split separator |> Seq.toList
    let data =
        match parts with
          | hd::tl::[] -> Some (separator + hd)
          | _ -> None
    data

[<EntryPoint>]
let main argv =
    printfn "%s" (download_reverso "aveuglement")
    0 // return an integer exit code
