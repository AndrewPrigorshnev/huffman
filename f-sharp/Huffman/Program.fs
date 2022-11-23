let compress (args: string[]) =
    $"The file {args[0]} is compressed to {args[1]}"

let expand (args: string[]) =
    $"The file {args[0]} is expanded to {args[1]}"
    
let unknownCommand _ =
    "Unknown command"
    
let run command =
    match command with
    | "compress" -> compress
    | "expand" -> expand
    | _ -> unknownCommand

[<EntryPoint>]
let main args =
    run args[0] args[1..]
    |> printfn "%s"

    0
