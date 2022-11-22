let compress =
    "The file is compressed"
    
let expand =
    "The file is expanded"

let command name =
    match name with
    | "compress" -> compress
    | "expand" -> expand
    | _ -> ()

[<EntryPoint>]
let main args =
    expand |> printfn "%s"
    0
    