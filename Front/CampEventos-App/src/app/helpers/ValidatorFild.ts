import { AbstractControl, FormGroup } from "@angular/forms";

export class ValidatorFild {

    static MustMatch(controlName: string, macthingControlName: string): any {

        return(group: AbstractControl) => {
            const FormGroup = group as FormGroup;

            const control = FormGroup.controls[controlName];
            const macthingControl = FormGroup.controls[macthingControlName];

            if (macthingControl.errors && !macthingControl.errors['mustMatch']) {
                return null;
            }

            if (control.value !== macthingControl.value) {
                macthingControl.setErrors({mustMatch: true});

            } else {
                macthingControl.setErrors(null);
            }

            return null;
        }
    }
}
