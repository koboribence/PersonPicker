import { Component } from '@angular/core';

@Component({
  selector: 'app-person-manager',
  standalone: false,
  templateUrl: './person-manager.component.html',
  styleUrl: './person-manager.component.scss'
})
export class PersonManagerComponent {

}
export interface Person {
  id: string;
  name: string;
}
