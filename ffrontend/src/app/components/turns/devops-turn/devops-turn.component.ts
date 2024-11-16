import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule, AbstractControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NavbarComponent } from '../../../commons/components/navbar/navbar.component';

interface DevOpsTask {
  category: string;
  description: string;
}

@Component({
  selector: 'app-devops-turn',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NavbarComponent],
  templateUrl: './devops-turn.component.html',
  styleUrls: ['./devops-turn.component.css']
})
export class DevOpsTurnComponent implements OnInit {
  turnForm!: FormGroup;

  devopsCategories = [
    'CI/CD Pipeline',
    'Container Orchestration',
    'Infrastructure as Code',
    'Monitoring & Logging',
    'Cloud Services',
    'Security & Compliance',
    'Version Control',
    'Database Management',
    'Network Configuration',
    'Deployment Strategies'
  ];

  toolCategories = {
    'CI/CD': ['Jenkins', 'GitLab CI', 'GitHub Actions', 'CircleCI', 'Travis CI'],
    'Containers': ['Docker', 'Kubernetes', 'Docker Compose', 'OpenShift'],
    'IaC': ['Terraform', 'Ansible', 'CloudFormation', 'Puppet'],
    'Monitoring': ['Prometheus', 'Grafana', 'ELK Stack', 'Nagios'],
    'Cloud': ['AWS', 'Azure', 'GCP', 'Digital Ocean'],
    'Version Control': ['Git', 'GitHub', 'GitLab', 'Bitbucket']
  };

  difficultyLevels = [
    { value: 'beginner', label: 'Beginner' },
    { value: 'intermediate', label: 'Intermediate' },
    { value: 'advanced', label: 'Advanced' },
    { value: 'expert', label: 'Expert' }
  ];

  defaultTasks: DevOpsTask[] = [
    { category: 'Infrastructure Setup', description: 'Initial environment configuration' },
    { category: 'Pipeline Creation', description: 'Build and deployment pipeline setup' },
    { category: 'Monitoring', description: 'Implement monitoring and alerting' },
    { category: 'Security', description: 'Security measures implementation' }
  ];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.initForm();
    const turnId = this.route.snapshot.paramMap.get('id');
    if (turnId) {
      this.loadTurnData(turnId);
    } else {
      this.defaultTasks.forEach(() => this.addTask());
    }
  }

  private initForm() {
    this.turnForm = this.fb.group({
      title: ['', Validators.required],
      category: ['', Validators.required],
      difficulty: ['intermediate', Validators.required],
      timeLimit: [120, [Validators.required, Validators.min(30)]],
      description: ['', [Validators.required, Validators.minLength(50)]],
      prerequisites: this.fb.array([]),
      environment: this.fb.group({
        platform: ['', Validators.required],
        requirements: ['', Validators.required],
        resources: ['', Validators.required],
        access: ['', Validators.required]
      }),
      tasks: this.fb.array([]),
      infrastructure: this.fb.group({
        architecture: ['', Validators.required],
        components: this.fb.array([]),
        constraints: ['', Validators.required]
      }),
      deliverables: this.fb.array([]),
      evaluation: this.fb.array([]),
      documentation: this.fb.group({
        required: [true],
        templates: this.fb.array([]),
        format: ['markdown', Validators.required]
      })
    });
  }

  private loadTurnData(turnId: string) {
    console.log('Loading DevOps turn data for ID:', turnId);
  }

  // Getters for form arrays
  get prerequisites() {
    return this.turnForm.get('prerequisites') as FormArray;
  }

  get tasks() {
    return this.turnForm.get('tasks') as FormArray;
  }

  get components() {
    return this.turnForm.get('infrastructure.components') as FormArray;
  }

  get deliverables() {
    return this.turnForm.get('deliverables') as FormArray;
  }

  get evaluation() {
    return this.turnForm.get('evaluation') as FormArray;
  }

  get documentationTemplates() {
    return this.turnForm.get('documentation.templates') as FormArray;
  }

  // Methods to add/remove form array items
  addPrerequisite() {
    const prerequisite = this.fb.group({
      tool: ['', Validators.required],
      version: ['', Validators.required],
      purpose: ['', Validators.required]
    });
    this.prerequisites.push(prerequisite);
  }

  removePrerequisite(index: number) {
    this.prerequisites.removeAt(index);
  }

  addTask() {
    const task = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      steps: this.fb.array([]),
      validation: ['', Validators.required],
      points: [10, [Validators.required, Validators.min(1), Validators.max(100)]]
    });
    this.tasks.push(task);
  }

  removeTask(index: number) {
    this.tasks.removeAt(index);
  }

  addTaskStep(taskIndex: number) {
    const task = this.tasks.at(taskIndex);
    const steps = task.get('steps') as FormArray;
    steps.push(this.fb.control('', Validators.required));
  }

  removeTaskStep(taskIndex: number, stepIndex: number) {
    const task = this.tasks.at(taskIndex);
    const steps = task.get('steps') as FormArray;
    steps.removeAt(stepIndex);
  }

  addComponent() {
    const component = this.fb.group({
      name: ['', Validators.required],
      type: ['', Validators.required],
      configuration: ['', Validators.required]
    });
    this.components.push(component);
  }

  removeComponent(index: number) {
    this.components.removeAt(index);
  }

  addDeliverable() {
    const deliverable = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      criteria: ['', Validators.required],
      format: ['', Validators.required]
    });
    this.deliverables.push(deliverable);
  }

  removeDeliverable(index: number) {
    this.deliverables.removeAt(index);
  }

  addEvaluationCriterion() {
    const criterion = this.fb.group({
      criterion: ['', Validators.required],
      weight: [0, [Validators.required, Validators.min(0), Validators.max(100)]],
      description: ['', Validators.required]
    });
    this.evaluation.push(criterion);
  }

  removeEvaluationCriterion(index: number) {
    this.evaluation.removeAt(index);
  }

  addDocumentationTemplate() {
    const template = this.fb.group({
      title: ['', Validators.required],
      content: ['', Validators.required],
      required: [true]
    });
    this.documentationTemplates.push(template);
  }

  removeDocumentationTemplate(index: number) {
    this.documentationTemplates.removeAt(index);
  }

  calculateTotalPoints(): number {
    return this.tasks.controls.reduce((sum, task) => 
      sum + (task.get('points')?.value || 0), 0);
  }

  calculateTotalWeight(): number {
    return this.evaluation.controls.reduce((sum, criterion) => 
      sum + (criterion.get('weight')?.value || 0), 0);
  }

  onSubmit() {
    if (this.turnForm.valid) {
      const totalPoints = this.calculateTotalPoints();
      const totalWeight = this.calculateTotalWeight();
      
      if (totalPoints !== 100) {
        alert('Total task points must equal 100');
        return;
      }

      if (totalWeight !== 100) {
        alert('Total evaluation criteria weight must equal 100');
        return;
      }

      console.log(this.turnForm.value);
      this.router.navigate(['/new-job']);
    } else {
      this.markFormGroupTouched(this.turnForm);
    }
  }

  private markFormGroupTouched(formGroup: FormGroup | FormArray) {
    Object.values(formGroup.controls).forEach(control => {
      if (control instanceof FormGroup || control instanceof FormArray) {
        this.markFormGroupTouched(control);
      } else {
        control.markAsTouched();
      }
    });
  }

  getStepsFormArray(task: AbstractControl): FormArray {
    return task.get('steps') as FormArray;
  }

  getTemplates(): FormArray {
    return this.turnForm.get('documentation.templates') as FormArray;
  }
  
  getTaskSteps(taskIndex: number): FormArray {
    return (this.tasks.at(taskIndex).get('steps') as FormArray);
  }
}