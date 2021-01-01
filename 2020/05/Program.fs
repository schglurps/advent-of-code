open System
open System.IO

type Location = Lower | Upper

let decode (s: string)   =
    let rec innerDecode (l: char list) min max (getLocation: char -> Location) =
        match l with
        | head::tail ->
            match getLocation(head) with
            | Lower -> 
                let newMax = (min + max) / 2
                innerDecode tail min newMax getLocation
            | Upper ->
                let newMin = 1 + (min + max) / 2
                innerDecode tail newMin max getLocation
        | [] -> min

    let rowList = s.Substring(0, 7) |> List.ofSeq
    let row = innerDecode rowList 0 127 (fun c -> if c = 'F' then Lower else Upper)

    let columnList = s.Substring(7) |> List.ofSeq
    let column = innerDecode columnList 0 7 (fun c -> if c = 'L' then Lower else Upper)

    row * 8 + column

[<EntryPoint>]
let main argv =
    let (seats, min, max) =
        File.ReadLines("input.txt")
        |> Seq.fold (fun acc cur ->
            let seat = decode cur
            let (currentSeats, currentMin, currentMax) = acc
            let newMin = min seat currentMin
            let newMax = max seat currentMax
            (seat::currentSeats, newMin, newMax)) ([], Int32.MaxValue, Int32.MinValue)

    seq { min..max }
    |> Seq.except (seats |> Seq.ofList)
    |> Seq.exactlyOne
    |> printfn "%d"

    0