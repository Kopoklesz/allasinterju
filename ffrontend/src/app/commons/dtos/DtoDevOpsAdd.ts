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
    architectrueDescription: string;
    infrastructureConstraints: string;
    documentationRequired: boolean;
    documentationFormat: string; 
    prerequisites: Array<BPrerequisites>;
    tasks: Array<BTask>;
    components: Array<BComponent>;
    deliverables: Array<BDeliverables>;
    evaluationCriteria: Array<BEvalCriteriaDevops>;
    docTemplates: Array<BDocumentationTemplate>;
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