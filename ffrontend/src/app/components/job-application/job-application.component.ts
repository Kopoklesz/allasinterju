import { Component, Renderer2, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { JobApplicationService } from '../../services/job-application/job-application.service';
import { DtoJob } from '../../commons/dtos/DtoJob';
import { DtoTest } from '../../commons/dtos/DtoTest';
import { parseJwt } from '../../utils/cookie.utils';
import { UserService } from '../../services/user/user.service'; 

@Component({
  selector: 'app-job-application',
  standalone: true,
  imports: [CommonModule, NavbarComponent],
  templateUrl: './job-application.component.html',
  styleUrls: ['./job-application.component.css']
})

export class JobApplicationComponent {
  job: DtoJob | null = null;
  isApplied: boolean = false;
  isLoading: boolean = false;
  jobTests: DtoTest[] = [];
  totalDuration: number = 0;
  activeMessages: HTMLElement[] = [];
  showConfirmDialog = false;
  isJobCreator: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private jobApplicationService: JobApplicationService,
    private userService: UserService,
    private renderer: Renderer2,
    private el: ElementRef 
  ) {}

  pageTitle = 'Job-Application';

  ngOnInit() {
  
    const jobIdParam = this.route.snapshot.paramMap.get('id'); 
    const jobId = jobIdParam ? Number(jobIdParam) : null;       
    if (jobId !== null) {
      this.jobApplicationService.getJob(jobId).subscribe(data => {
        this.job = data;
    
        this.checkApplicationStatus(jobId);
        this.checkJobCreator();
      });
    } else {
      console.error('Job ID is missing or invalid');
    }
  }

  checkApplicationStatus(jobId: number) {
   
        let id = parseJwt(localStorage.getItem("JWT_TOKEN"))?.id;
        let role = parseJwt(localStorage.getItem("JWT_TOKEN"))?.role;
        if(id){
          
            let idnum = +id;
            if(role === "Munkakereso"){
             
                    this.userService.getAppliedJob(idnum).subscribe({
                          next: (response) => {
                            for(let i = 0; i<=response.length; i++){
                                    if(response.at(i)?.id == idnum){
                                    this.isApplied = true;
                                    this.loadJobTests(this.job!.id);
                                    this.showSuccessMessage('Already applied for the job!');
                                    this.isLoading = false;
                                    break;
                                }
                            }
                          },
                          error: (error) => {
                                console.error('Error checking application status:', error);
                          }

                    });
            }
            
        }
  /*  this.jobApplicationService.checkApplicationStatus(jobId).subscribe({
      next: (status) => {
        this.isApplied = status;
        if (status) {
             this.loadJobTests(jobId);
        }
      },
      error: (error) => {
        console.error('Error checking application status:', error);
      }
    });*/
  }

  viewSubmittedApplications() {
    if (this.job) {
      this.router.navigate(['/submitted-applications', this.job.id]);
    }
  }

  private checkJobCreator() {
    const token = localStorage.getItem("JWT_TOKEN");
    if (token && this.job) {
      const decodedToken = parseJwt(token);
      this.isJobCreator = decodedToken?.role == "Ceg" && Number(decodedToken.id) === this.job?.company.id;
    }
  }

  editJob() {
    if (this.job) {
      this.router.navigate(['/edit-job', this.job.id]);
    }
  }

  loadJobTests(jobId: number) {
 /*   this.jobApplicationService.getJobTests(jobId).subscribe({
      next: (tests) => {
        this.jobTests = tests;
        this.totalDuration = tests.reduce((sum, test) => sum + test.duration, 0);
      },
      error: (error) => {
        console.error('Error loading job tests:', error);
      }
    });*/
  }

  applyForJob() {
    if (!this.job?.id) return;
    
    const token = localStorage.getItem("JWT_TOKEN");
    if (token) {
      const decodedToken = parseJwt(token);
      if (decodedToken?.role === 'Ceg') {
        this.showErrorMessage('Companies cannot apply for jobs');
        return;
      }
    }
    this.showConfirmDialog = true;
   }

  confirmApply() {
    if (!this.job?.id) return;
    
    this.showConfirmDialog = false;
    this.isLoading = true;
   
    this.jobApplicationService.applyForJob(this.job.id).subscribe({
      next: (response) => {
        this.isApplied = true;
        this.loadJobTests(this.job!.id);
        this.showSuccessMessage('Successfully applied for the job!');
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error applying for job:', error);
        this.showErrorMessage('Failed to apply for the job. Please try again later.');
        this.isLoading = false;
      }
    });
  }

  private showMessage(message: string, type: 'success' | 'error') {
    const messageElement = this.renderer.createElement('div');
    this.renderer.addClass(messageElement, 'message');
    this.renderer.addClass(messageElement, `message-${type}`);

    const icon = type === 'success' ? '✓' : '!';
    messageElement.innerHTML = `
      <div class="message-icon">${icon}</div>
      <span class="message-text">${message}</span>
      <button class="message-close">×</button>
    `;
    
    const closeButton = messageElement.querySelector('.message-close');
    if (closeButton) {
      this.renderer.listen(closeButton, 'click', () => {
        this.hideMessage(messageElement);
      });
    }

    this.renderer.appendChild(this.el.nativeElement.ownerDocument.body, messageElement);
    this.activeMessages.push(messageElement);
    this.positionMessages();

    setTimeout(() => {
      this.hideMessage(messageElement);
    }, 5000);
  }

  private hideMessage(messageElement: HTMLElement) {
    this.renderer.addClass(messageElement, 'hiding');

    setTimeout(() => {
      if (this.el.nativeElement.ownerDocument.body.contains(messageElement)) {
        this.renderer.removeChild(
          this.el.nativeElement.ownerDocument.body,
          messageElement
        );
        this.activeMessages = this.activeMessages.filter(m => m !== messageElement);
        this.positionMessages();
      }
    }, 300);
  }

  startTests() {
    if (!this.job?.id) return;
    this.router.navigate(['/job-tests', this.job.id]);
  }

  getCompletedTestsCount(): number {
    return this.jobTests.filter(test => test.isCompleted).length;
  }

  private showSuccessMessage(message: string) {
    this.showMessage(message, 'success');
  }
  
  private showErrorMessage(message: string) {
    this.showMessage(message, 'error');
  }

  private positionMessages() {
    let topOffset = 100;
    this.activeMessages.forEach(message => {
      this.renderer.setStyle(message, 'top', `${topOffset}px`);
      topOffset += message.offsetHeight + 10;
    });
  }

  ngOnDestroy() {
    this.activeMessages.forEach(message => {
      this.renderer.removeChild(
        this.el.nativeElement.ownerDocument.body,
        message
      );
    });
    this.activeMessages = [];
  }
}