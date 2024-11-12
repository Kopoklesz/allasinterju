import { Component } from '@angular/core';
import { NavbarComponent } from '../../commons/components/navbar/navbar.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-new-job',
  standalone: true,
  imports: [NavbarComponent,CommonModule,FormsModule],
  templateUrl: './new-job.component.html',
  styleUrl: './new-job.component.css'
})
export class NewJobComponent {
  job = {
    cim: '',
    leiras: '',
    munkarend: '',
    rovidleiras: '',
    telepely: '',
    kitoltesihatarido: ''
  };

  constructor(private router: Router, private activatedRoute: ActivatedRoute){

  }

  selectedTurn: string = '';
  turns: { name: string; count: number }[] = [];
  turnCount: { [key: string]: number } = {};


  onSubmit() {
    console.log('Job submitted:', this.job);
    
  }

  addNewTurn() {
    if (this.selectedTurn) {
      const currentCount = this.turnCount[this.selectedTurn] || 0;
      const newTurn = `${this.selectedTurn} ${currentCount + 1}`;
      this.turnCount[this.selectedTurn] = currentCount + 1;
      this.turns.push({ name: newTurn, count: currentCount + 1 });
      this.selectedTurn = '';
    }
  }

  editTurn(turnName: string) {
    this.router.navigate(['/edit-turns', turnName]);
  }
}