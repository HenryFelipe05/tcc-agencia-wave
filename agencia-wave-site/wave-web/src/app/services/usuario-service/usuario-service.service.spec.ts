import { TestBed } from '@angular/core/testing';

import { UsuarioService } from '../usuario-service/usuario-service.service';

describe('UsuarioServiceService', () => {
  let service: UsuarioService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UsuarioService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
