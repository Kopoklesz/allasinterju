import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NavbarComponent } from '../../../commons/components/navbar/navbar.component';
import { DtoKerdoivLetrehozas } from '../../../commons/dtos/DtoJob';
import { JobApplicationService } from '../../../services/job-application/job-application.service';
import { BDesignAdd } from '../../../commons/dtos/DtoDesignAdd';

interface DesignRequirement {
  category: string;
  description: string;
}

@Component({
  selector: 'app-design-turn',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NavbarComponent],
  templateUrl: './design-turn.component.html',
  styleUrls: ['./design-turn.component.css']
})
export class DesignTurnComponent implements OnInit {
  pageTitle: string = '';
  turnForm!: FormGroup;
  designCategories = [
    'UI Design',
    'UX Design',
    'Graphic Design',
    'Logo Design',
    'Web Design',
    'Mobile Design'
  ];

  defaultRequirements: DesignRequirement[] = [
    { category: 'Color Scheme', description: '' },
    { category: 'Typography', description: '' },
    { category: 'Layout', description: '' },
    { category: 'Responsive Design', description: '' }
  ];

  constructor(
    private jobApplicationService: JobApplicationService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.initForm();

    const turnName = this.route.snapshot.queryParamMap.get('name');
    if (turnName) {
      this.pageTitle = turnName;
    } else {
      const turnType = this.getTurnTypeFromRoute();
      this.pageTitle = `${turnType} Turn`;
    }

    const turnId = this.route.snapshot.paramMap.get('id');
    if (turnId) {
      this.loadTurnData(turnId);
    } else {
      this.defaultRequirements.forEach(() => this.addRequirement());
    }
  }

  private getTurnTypeFromRoute(): string {
    const currentRoute = this.router.url;
    if (currentRoute.includes('programming')) return 'Programming';
    if (currentRoute.includes('design')) return 'Design';
    if (currentRoute.includes('algorithms')) return 'Algorithms';
    if (currentRoute.includes('testing')) return 'Testing';
    if (currentRoute.includes('devops')) return 'DevOps';
    return 'Turn';
  }

  private initForm() {
    this.turnForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', [Validators.required, Validators.minLength(10)]],
      category: ['UI Design', Validators.required],
      timeLimit: [120, [Validators.required, Validators.min(30)]],
      designRequirements: this.fb.array([]),
      styleGuide: ['', Validators.required],
      deliverables: ['', Validators.required],
      referenceLinks: this.fb.array([]),
      evaluationCriteria: this.fb.array([])
    });
  }

  private loadTurnData(turnId: string) {
    // Betöltés az állás adataival (ha folytatjuk)
    console.log('Loading turn data for ID:', turnId);
  }

  get designRequirements() {
    return this.turnForm.get('designRequirements') as FormArray;
  }

  get referenceLinks() {
    return this.turnForm.get('referenceLinks') as FormArray;
  }

  get evaluationCriteria() {
    return this.turnForm.get('evaluationCriteria') as FormArray;
  }

  addRequirement() {
    const requirement = this.fb.group({
      category: ['', Validators.required],
      description: ['', Validators.required]
    });
    this.designRequirements.push(requirement);
  }

  removeRequirement(index: number) {
    this.designRequirements.removeAt(index);
  }

  addReferenceLink() {
    const link = this.fb.group({
      description: ['', Validators.required],
      url: ['', [Validators.required, Validators.pattern('https?://.+')]]
    });
    this.referenceLinks.push(link);
  }

  removeReferenceLink(index: number) {
    this.referenceLinks.removeAt(index);
  }

  addEvaluationCriterion() {
    const criterion = this.fb.group({
      criterion: ['', Validators.required],
      weight: [0, [Validators.required, Validators.min(0), Validators.max(100)]]
    });
    this.evaluationCriteria.push(criterion);
  }

  removeEvaluationCriterion(index: number) {
    this.evaluationCriteria.removeAt(index);
  }

  onSubmit() {
    if (this.turnForm.valid) {
      console.log("sdadsa")
      console.log(this.turnForm.value);
      let kerdoiv: BDesignAdd = {
        jobId: 10, // Use the relevant jobId if available
        name: this.turnForm.get('title')?.value,
        round: 1, // Set round value if needed (e.g., from a form control or static value)
        title: this.turnForm.get('title')?.value,
        category: this.turnForm.get('category')?.value,
        description: this.turnForm.get('description')?.value,
        timeLimit: this.turnForm.get('timeLimit')?.value,
        designRequirements: (this.turnForm.get('designRequirements') as FormArray).controls.map(req => ({
          category: req.get('category')?.value,
          description: req.get('description')?.value
        })),
        styleGuide: this.turnForm.get('styleGuide')?.value,
        deliverables: this.turnForm.get('deliverables')?.value,
        referenceLinks: (this.turnForm.get('referenceLinks') as FormArray).controls.map(link => ({
          description: link.get('description')?.value,
          url: link.get('url')?.value
        })),
        evalCriteria: (this.turnForm.get('evaluationCriteria') as FormArray).controls.map(criterion => ({
          description: criterion.get('description')?.value,
          weight: criterion.get('weight')?.value
        }))
      };
    console.log(kerdoiv);
    this.jobApplicationService.addDesign(kerdoiv).subscribe({
      next: (response) =>{
          
      },
      error: (error) => {
          console.log(error)
      }

    });
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

  finish() {
    const returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
    if (returnUrl) {
      this.router.navigateByUrl(returnUrl);
    }
  }
}