/*tslint:disable:no-console */
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app/app.module';
import { EpAppInitializer } from '@epicor/kinetic';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}


EpAppInitializer.initialize(() => {
  platformBrowserDynamic()
    .bootstrapModule(AppModule)
    .catch(err => console.log(err));
});
