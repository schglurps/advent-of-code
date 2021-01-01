open System.IO

type Slope = { Right: int; Down: int }
type State = { X: int; Trees: int; LinesToSkip: int }

let move state (line: string) slope =
    match state.LinesToSkip with
    | linesToSkip when linesToSkip > 0 -> { state with LinesToSkip = state.LinesToSkip - 1 }
    | _ ->
        let x = (state.X + slope.Right) % line.Length
        let trees = if line.[x] = '#' then state.Trees + 1 else state.Trees
        { state with X = x; Trees = trees; LinesToSkip = slope.Down - 1 }

[<EntryPoint>]
let main argv =
    [(1, 1); (3, 1); (5, 1); (7, 1); (1, 2)]
    |> List.map (fun (right, down) ->
        let slope = { Right = right; Down = down }
        let finalState =
            File.ReadLines("input.txt")
            |> Seq.fold (fun acc cur -> move acc cur slope) { X = 0; Trees = 0; LinesToSkip = slope.Down }
        finalState.Trees)
    |> List.fold (*) 1
    |> printfn "%d"

    0