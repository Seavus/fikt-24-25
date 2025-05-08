import { AppComponent } from './app/app.component';
import { authInterceptor } from './app/auth-interceptor.service';
import { appConfig } from './app/app.config';
import { bootstrapApplication } from '@angular/platform-browser';

bootstrapApplication(AppComponent, {
  ...appConfig,
  providers: [provideHttpClient(withInterceptors([authInterceptor]))],
}).catch((err) => console.error(err));
