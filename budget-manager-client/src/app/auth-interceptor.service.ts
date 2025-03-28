import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const userData = localStorage.getItem('userData');
  let token = '';

  if (userData) {
    const parsedData = JSON.parse(userData);
    token = parsedData?.token || '';
  }

  if (token) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  return next(req);
};
