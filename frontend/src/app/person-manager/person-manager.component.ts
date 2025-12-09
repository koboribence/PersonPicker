import { Component, OnInit } from '@angular/core';
import { PersonService } from '../person.service';

@Component({
  selector: 'app-person-manager',
  standalone: false,
  templateUrl: './person-manager.component.html',
  styleUrl: './person-manager.component.scss'
})
export class PersonManagerComponent implements OnInit {

  persons: Person[] = [];
  draws: Draw[] = [];
  newName = '';
  selectedPerson: Person | null = null;
  errorMessage = '';
  isLoading = false;

  constructor(private personService: PersonService) { }

  ngOnInit(): void {
    this.loadPersons();
    this.loadDraws();
  }

  loadPersons(): void {
    this.personService.getAll().subscribe({
      next: persons => {
        this.persons = persons;
      },
      error: err => {
        this.errorMessage = 'Hiba a nevek betöltésekor.';
        console.error(err);
      }
    });
  }

  loadDraws(): void {
    this.personService.getDraws().subscribe({
      next: draws => {
        this.draws = draws;
      },
      error: err => {
        console.error('Hiba a sorsolások betöltésekor.', err);
      }
    });
  }
  
  addPerson(): void {
    this.errorMessage = '';
    this.selectedPerson = null;

    const name = this.newName.trim();
    if (!name) {
      this.errorMessage = 'A név nem lehet üres.';
      return;
    }

    this.personService.add(name).subscribe({
      next: person => {
        this.persons.push(person);
        this.newName = '';
      },
      error: err => {
        this.errorMessage = 'Hiba a név mentésekor.';
        console.error(err);
      }
    });
  }

  deletePerson(person: Person): void {
    this.errorMessage = '';

    this.personService.delete(person.id).subscribe({
      next: () => {
        this.persons = this.persons.filter(p => p.id !== person.id);
      },
      error: err => {
        this.errorMessage = 'Hiba a törlés közben.';
        console.error(err);
      }
    });
  }

drawRandom(): void {
  this.errorMessage = '';
  this.selectedPerson = null;
  this.isLoading = true;

  this.personService.drawRandom().subscribe({
    next: person => {
      this.selectedPerson = person;
      this.isLoading = false;
      this.loadDraws();   // <<< ITT frissítjük a sorsolási listát
    },
    error: err => {
      if (err.status === 404) {
        this.errorMessage = 'Nincs név az adatbázisban.';
      } else {
        this.errorMessage = 'Hiba a sorsolás közben.';
      }
      this.isLoading = false;
      console.error(err);
    }
  });
}
}

export interface Person {
  id: string;
  name: string;
}

export interface Draw {
  id: string;
  personName: string;
  selectedAt: string;
}
