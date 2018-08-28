import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

//modules
import { SharedModule } from '../../app.module.shared';
import { OrthodonticsRoutingModule } from './orthodontics.routing.module';

//Components
import { OrthodonticsComponent } from './orthodontics.component';
import { MeasureComponent } from './measures/measures.component';
import { MeasuresListComponent } from './measures/measures-list.component';
import { ResultsComponent } from './results/results.component'
import { ResultPdfComponent } from './results/result-pdf.component';

//Services
import { ConfigService } from '../../services/config.service';
import { AuthGuard } from "../../services/auth-guard.service";
import { MeasuresService } from '../../services/measures.service';

//Pipes
import { DateFormatPipe } from '../shared/pipes/date-transform.pipe';

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
        MeasuresListComponent,
        ResultsComponent,
        ResultPdfComponent
    ],
    providers: [
        { provide: 'authorize', useValue: true },
        MeasuresService,
        ConfigService,
        AuthGuard,
        DateFormatPipe
    ]
})
export class OrthodonticsModule { }