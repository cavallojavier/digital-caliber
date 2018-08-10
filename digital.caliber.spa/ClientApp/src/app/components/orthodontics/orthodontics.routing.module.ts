import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrthodonticsComponent } from './orthodontics.component';
import { ResultsComponent } from './results/results.component';
import { MeasureComponent } from './measures/measures.component';
import { MeasuresListComponent } from './measures/measures-list.component';
import { AuthGuard } from '../../services/auth-guard.service';
import { ResultPdfComponent } from './results/result-pdf.component';

const orthodonticsRoutes: Routes = [
    {
        path: 'orthodontics',
        canActivate: [AuthGuard],
        //component: OrthodonticsComponent,
        children: [
            { path: '',  component: OrthodonticsComponent}, 
            { path: 'list', component: MeasuresListComponent },
            { path: 'measures', component: MeasureComponent },
            { path: 'measures/:id', component: MeasureComponent },
            { path: 'results/:id', component: ResultsComponent },
            { path: 'resultsPdf/:id', component: ResultPdfComponent },
            { path: '**', redirectTo: '' },
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(orthodonticsRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class OrthodonticsRoutingModule { }