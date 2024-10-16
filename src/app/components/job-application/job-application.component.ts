import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-job-application',
  standalone: true,
  imports: [CommonModule, NavbarComponent],
  templateUrl: './job-application.component.html',
  styleUrls: ['./job-application.component.css']
})
export class JobApplicationComponent {
  job = {
    company_name: 'Company A',
    job_position: 'Developer',
    long_desc: `
    We are seeking a talented and passionate Developer to join our dynamic team. 
    As a Developer at Company A, you will be responsible for designing, developing, and maintaining high-quality software solutions that meet the needs of our clients. 
    You will collaborate closely with project managers, designers, and other developers to create innovative applications that enhance user experience and functionality.

    Your key responsibilities will include:
    - Writing clean, scalable, and efficient code
    - Troubleshooting, debugging, and upgrading existing software
    - Collaborating with cross-functional teams to define, design, and ship new features
    - Participating in code reviews and providing constructive feedback
    - Staying up to date with emerging technologies and industry trends

    The ideal candidate will have a strong background in software development and a passion for technology. 
    You should be comfortable working in a fast-paced environment and be able to adapt to new challenges. 
    We value creativity and initiative, and we encourage our team members to think outside the box to find innovative solutions to complex problems.

    In addition to your technical skills, you should possess excellent communication and interpersonal skills. 
    You will need to be able to effectively communicate your ideas and collaborate with others to achieve common goals.

    Company A is committed to providing a supportive and inclusive work environment. 
    We offer opportunities for professional development and career advancement, as well as a competitive salary and benefits package. 
    If you are looking for a place where you can grow your career and make a meaningful impact, we would love to hear from you!
  `,
  expectations: `
   We are looking for a candidate who is self-motivated and has a passion for technology. 
  The ideal candidate should have a strong understanding of software development principles, 
  and experience in programming languages such as Java, Python, or JavaScript. You should be 
  comfortable working in a fast-paced environment and be able to handle multiple tasks simultaneously. 
  We expect you to demonstrate effective problem-solving skills, and be able to think critically 
  about complex challenges. 

  Additionally, familiarity with web development frameworks and technologies such as React, Angular, 
  or Vue.js will be beneficial. Knowledge of databases (SQL and NoSQL), cloud services (AWS, Azure, 
  Google Cloud), and DevOps practices are also advantageous. 

  Strong communication skills are essential as you will collaborate with cross-functional teams, 
  share ideas, and contribute to group discussions. You should be able to articulate your thoughts 
  clearly and concisely, both verbally and in writing. 

  You will be expected to stay updated with the latest industry trends and technologies, continuously 
  seeking opportunities to improve your skills. A proactive approach to learning and development 
  is crucial as we aim to foster a culture of innovation and growth.

  Attention to detail is key; you should be able to deliver high-quality work that meets project 
  specifications and timelines. Being adaptable and open to feedback will enhance your performance 
  and help you integrate smoothly into our team. 

  We also value a positive attitude and teamwork. You should be able to support your colleagues, 
  share knowledge, and contribute to a friendly and productive work environment. 

  As part of our commitment to work-life balance, we encourage our employees to manage their time 
  effectively and seek support when needed. We believe in the importance of mental health and well-being 
  and strive to create a supportive atmosphere for everyone. 

  In summary, we expect our candidates to possess technical skills, strong communication abilities, 
  a willingness to learn, attention to detail, and a collaborative mindset. If you embody these qualities, 
  we would love to have you on our team.
  `,
    image: 'https://via.placeholder.com/600x300'
  };
  pageTitle = 'Job Application';

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    const jobId = this.route.snapshot.paramMap.get('id');
    // Uncomment this section when backend is ready
    /*
    this.jobService.getJobById(jobId).subscribe(data => {
      this.job = data; // Update the job object with actual data
    });
    */
  }
}
