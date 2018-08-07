import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

//modules
import { SharedModule } from '../../app.module.shared';
import { OrthodonticsRoutingModule } from './orthodontics.routing.module';

//Components
import { OrthodonticsComponent } from './orthodontics.component';
import { MeasureComponent } from './measures/measures.component';
import { PatientsComponent } from './patients/patients.component';
import { ResultsComponent } from './results/results.component'

//Services
import { ConfigService } from '../../services/config.service';
import { AuthGuard } from "../../services/auth-guard.service";
import { MeasuresService } from '../../services/measures.service';

@NgModule({

    imports: [
        FormsModule,
        CommonModule,
        SharedModule,
        OrthodonticsRoutingModule
    ],
    declarations: [
        OrthodonticsComponent,
        MeasureComponent,
        PatientsComponent,
        ResultsComponent
    ],
    providers: [
        { provide: 'authorize', useValue: true },
        MeasuresService,
        ConfigService,
        AuthGuard
    ]
})
export class OrthodonticsModule { }