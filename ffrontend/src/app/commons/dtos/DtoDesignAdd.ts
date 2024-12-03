export interface BDesignAdd {
    jobId: number;
    name: string;
    round: number;
    title: string;
    category: string;
    description: string;
    timeLimit: number;
    designRequirements: BRequirement[];
    styleGuide: string;
    deliverables: string;
    referenceLinks: BRefLinks[];
    evalCriteria: BEvalCriteria[];
  }
  
  export interface BRequirement {
    category: string;
    description: string;
  }
  
  export interface BRefLinks {
    description: string;
    url: string;
  }
  
  export interface BEvalCriteria {
    description: string;
    weight: number;
  }