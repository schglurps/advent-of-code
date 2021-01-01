open System
open System.Text.RegularExpressions
open System.IO

type Entry = { Position1: int; Position2: int; Letter: char; Password: string }

let createEntry input =
        let regex = Regex("^(?<position1>\d+)-(?<position2>\d+) (?<letter>[a-z]): (?<password>[a-z]+)$")
        let m = regex.Match(input)
        let position1 = m.Groups.["position1"].Value |> Int32.Parse
        let position2 = m.Groups.["position2"].Value |> Int32.Parse
        let letter = m.Groups.["letter"].Value.[0]
        let password = m.Groups.["password"].Value
        { Position1 = position1 - 1; Position2 = position2 - 1; Letter = letter; Password = password }

let isValid entry =
    let xor a b = (a || b) && (a <> b)
    xor (entry.Password.[entry.Position1] = entry.Letter) (entry.Password.[entry.Position2] = entry.Letter)

[<EntryPoint>]
let main argv =    
    File.ReadLines("input.txt")
    |> Seq.fold(fun acc cur ->
        let entry = createEntry cur
        if isValid entry then acc + 1 else acc) 0
    |> printfn "%d"
    0