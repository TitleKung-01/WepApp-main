import { Component, Input } from '@angular/core';
import { faEnvelope, faHeart, faUser } from '@fortawesome/free-solid-svg-icons';
import { Member } from 'src/app/_models/member';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css'],
})
export class MemberCardComponent {
  faUser = faUser;
  faHeart = faHeart;
  faEnvelope = faEnvelope;
  @Input() member: Member | undefined;
}
