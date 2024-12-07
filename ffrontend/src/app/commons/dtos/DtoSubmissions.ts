export interface AlgorithmSolutionSubmission {
    testId: number;
    solution: string;
    timeComplexity: string;
    spaceComplexity: string;
}

export interface ProgrammingSolutionSubmission {
    testId: number;
    solution: string;
    language: string;  // Csak a nyelv megadása szükséges
}

export interface DesignSolutionSubmission {
    testId: number;
    solution: string;  // design leírása vagy esetleg URL
}

export interface TestingSolutionSubmission {
    testId: number;
    testResults: Array<{
        testCaseId: number;
        passed: boolean;
        notes?: string;
    }>;
}

export interface DevOpsSolutionSubmission {
    testId: number;
    solution: string;
    deploymentLog?: string;
}