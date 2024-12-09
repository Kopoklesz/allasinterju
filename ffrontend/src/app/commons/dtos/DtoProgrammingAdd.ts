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