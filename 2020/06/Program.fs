open System.IO

type State = { Data: Set<char>; PersonCount: int }
let createEmptyState() = { Data = Set.empty; PersonCount = 0 }

[<EntryPoint>]
let main argv =
    use sr = new StreamReader("input.txt")
    seq {
        let mutable state = createEmptyState()
        while not sr.EndOfStream do
            let line = sr.ReadLine()
            if line.Length <> 0 then
                let set = line |> Set.ofSeq
                state <-
                    match state.PersonCount with
                    | 0 -> { Data = set; PersonCount = 1 }
                    | _ -> { Data = Set.intersect state.Data set; PersonCount = state.PersonCount + 1 }
            else
                yield state
                state <- createEmptyState()

        if state.PersonCount <> 0 then
            yield state
    }
    |> Seq.sumBy (fun state -> state.Data.Count)
    |> printfn "%d"

    0