import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { AuthInterceptorService } from './shared/interceptor/auth-interceptor.service';

@NgModule({
    providers: [{provide: HTTP_INTERCEPTORS, useClass:AuthInterceptorService,multi:true}]
})
export class CoreModule {}