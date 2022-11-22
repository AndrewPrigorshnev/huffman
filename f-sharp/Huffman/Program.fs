let compress source destination =
    $"The file {source} is compressed to {destination}"

let expand source destination =
    $"The file {source} is expanded to {destination}"

let commands =
    [ "compress", compress
      "expand", expand ]
    |> dict

[<EntryPoint>]
let main args =
    commands[args[0]] args[1] args[2]
    |> printfn "%s"

    0
