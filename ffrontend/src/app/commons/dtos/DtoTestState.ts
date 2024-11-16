export interface DtoTestState {
    id: number;
    jobApplicationId: number;
    testId: number;
    isCompleted: boolean;
    answers: string;  // JSON 
    lastModified: Date;
  }