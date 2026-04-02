import { Component } from '@angular/core';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { JobCardComponent } from '../../commons/components/job-card/job-card.component';
import { CommonModule } from '@angular/common';
import { DtoJobShort } from '../../commons/dtos/DtoJobShort';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../services/user/user.service';
import { DtoUser, DtoUserLeetStats } from '../../commons/dtos/DtoUser';
import { FormsModule } from '@angular/forms';
import { BCompetence } from '../../commons/dtos/DtoCompetence';
import { CompetenceService } from '../../services/competence/competence.service';
import { response } from 'express';
import { error } from 'console';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [NavbarComponent ,JobCardComponent,CommonModule,FormsModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent {
  jobs: DtoJobShort[] = [];
  user?: DtoUser;
  leetcodeStats: DtoUserLeetStats | undefined;
  leetCodeUsername: string = '';
  isConnecting: boolean = false;
  connectionMessage: string = '';
  documentDescription: string = '';
  documentFileName: string = '';
  selectedFile: File | null = null;
  isUploading: boolean = false;
  uploadMessage: string = '';
  uploadError: boolean = false;
  competences : BCompetence[] = [];
  newCompetence : BCompetence ={
    id: 0,
    type: "",
    level: ""
  };
  showCompetencePopup : Boolean = false;

  constructor(
    private route: ActivatedRoute,
    private userService:  UserService,
    private router: Router,
    private competenceService : CompetenceService
  ) {}
  ngOnInit() {
    const userIdParam = this.route.snapshot.paramMap.get('id'); 
    const userId = userIdParam ? Number(userIdParam) : null; 
    console.log(userId);    
    if (userId !== null) {
      this.userService.getUserData(userId).subscribe(data =>{
        this.user = data;
      });
      this.userService.getAppliedJob(userId).subscribe(data => {
        this.jobs = data;
        console.log(this.jobs);
      });
      if (userId) {
        this.userService.getLeetcodeStats(userId).subscribe(data => {
          this.leetcodeStats = data;
        });
      }
      this.loadCompetences();
    } else {
      console.error('Job ID is missing or invalid');
    }
  }

  editProfile() {
    if (this.user) {
      this.router.navigate(['/edit-profile', this.user.id]);
    }
  }

  connectLeet() {
    if (!this.leetCodeUsername || this.isConnecting) return;

    this.isConnecting = true;
    this.connectionMessage = '';

    this.userService.connectLeetCode(this.leetCodeUsername).subscribe({
      next: (response) => {
        this.connectionMessage = 'LeetCode account successfully connected!';
        this.leetCodeUsername = '';
        this.loadUserData();
      },
      error: (error) => {
        this.connectionMessage = 'Failed to connect LeetCode account. Please try again.';
        console.error('Error connecting LeetCode account:', error);
        this.isConnecting = false;
      },
      complete: () => {
        this.isConnecting = false;
      }
    });
  }

  loadUserData() {
    if (this.user?.id) {
      this.userService.getLeetcodeStats(this.user.id).subscribe(data => {
        this.leetcodeStats = data;
      });
    }
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }

  uploadDocument() {
    if (!this.selectedFile || !this.documentDescription || !this.documentFileName || this.isUploading) {
      return;
    }
  
    this.isUploading = true;
    this.uploadMessage = '';
  
    // Debug információk
    console.log('Uploading file:', {
      description: this.documentDescription,
      fileName: this.documentFileName,
      file: this.selectedFile,
      fileSize: this.selectedFile.size,
      fileType: this.selectedFile.type
    });
  
    this.userService.uploadDocument(
      this.documentDescription,
      this.documentFileName,
      this.selectedFile
    ).subscribe({
      next: (response) => {
        console.log('Upload response:', response);  // Debug információ
        this.uploadMessage = 'Document uploaded successfully!';
        this.uploadError = false;
        this.resetUploadForm();
      },
      error: (error) => {
        console.log('Upload error details:', error);  // Debug információ
        this.uploadMessage = `Failed to upload document: ${error.error?.message || 'Please try again.'}`;
        this.uploadError = true;
      },
      complete: () => {
        this.isUploading = false;
      }
    });
  }

  private resetUploadForm() {
    this.documentDescription = '';
    this.documentFileName = '';
    this.selectedFile = null;
    const fileInput = document.getElementById('file') as HTMLInputElement;
    if (fileInput) {
      fileInput.value = '';
    }
  }

  showPopUp(){
    this.showCompetencePopup = true;
  }
  
  saveCompetence(){
    if(this.newCompetence.type && this.newCompetence.level) {
      this.competenceService.addToUser(this.newCompetence)
      .subscribe({
          next: (response) => {
              this.loadCompetences(); 
              this.resetCompetenceForm(); 
              this.closePopup();
          },
          error: (error) => {
              console.error('Error adding competence:', error);
          }
      });
    }
  } 

  closePopup(){
    this.showCompetencePopup = false;
  }

  loadCompetences(){
    this.competenceService.getForUser().subscribe({
      next: (response) => {
        
          this.competences = response;
          console.log(this.competences)
      },
      error: (error) => {

      }
    });
  }

  deleteCompetence(id: number){
    if(id) {
        this.competenceService.deleteforUser(id).subscribe({
            next: (response) => {
                this.loadCompetences(); // Frissítjük a listát sikeres törlés után
            },
            error: (error) => {
                console.error('Error deleting competence:', error);
            }
        });
    }
}

  private resetCompetenceForm() {
    this.newCompetence = {
        id: 0,
        type: "",
        level: ""
    };
}
}
