import { input, output } from "@angular/core";


export interface BAlgorithmAdd{

    jobId : number;
    name : string;
    round : number;
    title : string;
    category : string;
    difficulty : string;
    timeLimit : number;
    description : string;
    inputFormat : string;
    outputFormat : string;
    timeComplexity : string;
    spaceComplexity : string;
    sampleSolution : string;
    constraints : Array<string>;
    examples : Array<BExample>;
    hints : Array<string>;
    testCases : Array<BTestCaseAlgorithm>;
}
export interface BExample{
    input : string;
    output : string;
    explanation : string;
}

export interface BTestCaseAlgorithm{
    input : string;
    output : string;
    hidden : boolean;
    points : number;
}

export interface finishProg{
    kerdoivId : number;
    programkod : string;
}