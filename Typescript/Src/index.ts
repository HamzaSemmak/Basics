
const enum Level {
    Kid = 15,
    Easy = 9,
    Medium = 6,
    Hard = 3,
}


let lvl: string = "Easy";

if(lvl == "Easy")
{
    console.log(`The Level Is ${lvl} And Number Of Seconds Is ${Level.Easy}`);
}