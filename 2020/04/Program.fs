open System
open System.IO
open System.Text.RegularExpressions

type Passport = Map<string, string>
type Field = { Name: string; IsValid: string -> bool }

let checkYearField (s: string) min max = 
    let (success, value) = Int32.TryParse s
    if not success then
        false
    else
        value >= min && value <= max

let mandatoryFields = [
    {
        Name = "byr";
        IsValid = fun s -> checkYearField s 1920 2002
    };
    {
        Name = "iyr";
        IsValid = fun s -> checkYearField s 2010 2020
    };
    {
        Name = "eyr";
        IsValid = fun s -> checkYearField s 2020 2030
    };
    {
        Name = "hgt";
        IsValid = fun s ->
            let regex = Regex("^(?<number>\d+)(?<unit>cm|in)$")
            let m = regex.Match(s)
            if not m.Success then
                false
            else
                let number = m.Groups.["number"].Value |> Int32.Parse
                let unit = m.Groups.["unit"].Value
                if unit = "cm" then
                    number >= 150 && number <= 193
                else
                    number >= 59 && number <= 76
    };
    {
        Name = "hcl";
        IsValid = fun s -> Regex.IsMatch(s, "^#[0-9a-z]{6}$")
    };
    {
        Name = "ecl";
        IsValid = fun s -> Regex.IsMatch(s, "^(amb|blu|brn|gry|grn|hzl|oth)$")
    };
    {
        Name = "pid";
        IsValid = fun s -> Regex.IsMatch(s, "^\d{9}$")
    };
]


let isValid (passport: Passport) =
    mandatoryFields
    |> List.forall (fun f ->
        match passport.TryFind f.Name with
        | Some value -> f.IsValid value
        | None -> false)

let readPassports (sr: StreamReader) = 
    let fields = mandatoryFields |> List.map (fun f -> f.Name) |> String.concat "|"
    let regex = Regex($"(?<field>{fields}):(?<value>\S+)")

    let extractFromLine (line: string) =
        regex.Matches(line)
        |> Seq.cast<Match>
        |> Seq.map(fun m -> (m.Groups.["field"].Value, m.Groups.["value"].Value))

    seq {
        let mutable currentPassport: Passport = Map.empty
        while not sr.EndOfStream do
            let line = sr.ReadLine()
            if line.Length = 0 then
                yield currentPassport
                currentPassport <- Map.empty
            else
                extractFromLine line
                |> Seq.iter (fun t ->
                    let (key, value) = t
                    currentPassport <- currentPassport.Add (key, value))

        if currentPassport.Count > 0 then
            yield currentPassport
    }

[<EntryPoint>]
let main argv =
    use sr = new StreamReader("input.txt")

    readPassports sr
    |> Seq.fold (fun acc cur -> if isValid cur then acc + 1 else acc) 0
    |> printfn "%d"

    0