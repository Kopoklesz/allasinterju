export interface BBProgrammingAdd{
    jobId : number;
    name : string;
    round : number;
    title : string;
    description : string;
    language : string;
    timeLimit : number;
    codeTemplate : string;
    testCases : Array<BTestCase>;

}


export interface BTestCase{
    input : string;
    expectedOutput : string;
}



export interface RSolveP{

    kerdoivId : number;
    title : string;
    description : string;
    language : string;
    codeTemplate : string;
    kezdesIdo : Date;
    befejezesIdo : Date;
    kitoltesPerc : number;

}