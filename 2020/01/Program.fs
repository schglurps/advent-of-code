open System
open System.IO

[<EntryPoint>]
let main argv =
    let entries = 
        File.ReadLines("input.txt")
        |> Seq.map Int32.Parse
        |> Array.ofSeq

    let (entry1, entry2, entry3) =
        seq {
            for i in 0..entries.Length - 1 do
                for j in i + 1 .. entries.Length - 1 do
                    for k in j + 1 .. entries.Length - 1 do
                        yield (entries.[i], entries.[j], entries.[k])
        }
        |> Seq.find (fun (e1, e2, e3) -> e1 + e2 + e3 = 2020)

    printfn "%d" (entry1 * entry2 * entry3)
    0