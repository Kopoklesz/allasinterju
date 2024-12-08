export interface BDevOpsAdd {
    jobId: number;
    name: string;
    round: number;
    title: string;
    category: string;
    difficulty: string;
    description: string;
    timeLimit: number;
    platform: string;
    systemRequirements: string;
    resourceLimits: string;
    accessRequirements: string;
    architectureDescription: string;
    infrastructureConstraints: string;
    documentationRequired: boolean;
    documentationFormat?: string; 
    prerequisites: BPrerequisites[];
    tasks: BTask[];
    components: BComponent[];
    deliverables: BDeliverables[];
    evaluationCriteria: BEvalCriteriaDevops[];
    docTemplates: BDocumentationTemplate[];
    expirationTime: string;
  }
  
  export interface BPrerequisites {
    tool: string;
    version: string;
    purpose: string;
  }
  
  export interface BTask {
    title: string;
    description: string;
    steps: string[];
    validation: string;
    points: number;
  }
  
  export interface BComponent {
    name: string;
    type: string;
    configuration: string;
  }
  
  export interface BDeliverables {
    title: string;
    description: string;
    acceptance: string;
    format: string;
  }
  
  export interface BEvalCriteriaDevops {
    title: string;
    weight: number;
    description: string;
  }
  
  export interface BDocumentationTemplate {
    title: string;
    content: string;
    required: boolean;
  }