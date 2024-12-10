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
    kerdoId : number;
    title : string;
    description : string;
    language : string;
    codeTemplate : string;
    kezdesIdo : Date;
    befejezesIdo : Date;
    kitoltesPerc : number;
}

export interface RKitoltottPTeszteset{
    bemenet : string;
    elvartKimenet : string;
    stderr : string;
    memoriaMB : number;
    futasidoms : number;
    helyes : boolean;
    ellenorizve : boolean;
    nemfutle : boolean;
}

export interface RKitoltottP{
    kerdoivId: number;
    title: string;
    description: string;
    language?: string;
    codetemplate?: string;
    kezdesIdo: Date;
    befejezesIdo: Date;
    kitoltesPerc: number;
    tesztesetek: RKitoltottPTeszteset[];
}