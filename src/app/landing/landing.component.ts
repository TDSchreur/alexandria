import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'alexandria-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LandingComponent {
  message = '';

  constructor(private apiService: ApiService) {}

  getData() {
    this.apiService.getData().subscribe((data) => {
      this.message = data.message;
    });
  }
}
