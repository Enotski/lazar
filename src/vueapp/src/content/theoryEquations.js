export const TheoryEquations = {
  e_1: [
    `s(t) = s(t+kT) \\tag{1.1}`,
    `s(t) = A \\cdot cos(\\frac{2\\pi\\cdot t}{T} +\\phi) \\tag{1.2}`,
    `F_s = \\frac{1}{T_s} > 2F_a \\tag{1.3}`,
    `F_s = \\frac{1}{T_s} > 2\\Delta f \\tag{1.4}`,
    `\\sigma(nT) = \\delta(nT) + \\delta(nT - T) + \\delta(nT - 2T) + \\cdots \\tag{1.5}`,
    `\\delta(nT) = \\sigma(nT) - \\sigma(nT - T) \\tag{1.6}`,
    `\\sigma(nT) = \\sum_{k=0}^{+\\infty}\\delta(nT-kT) ) \\tag{1.7}`,
    `x(nT) = \\sum_{k=0}^{+\\infty}x(kT)\\cdot\\delta(nT-kT) ) \\tag{1.8}`,
    `X(z) = Z |x(nT)| = \\sum_{n=0}^{\\infty}x(nT)z^{-n} ) \\tag{1.9}`,
    `z = e^{sT} \\tag{1.10}`,
    `x(nT) = a \\cdot x_1(nT) + b \\cdot x_2(nT) \\tag{1.11}`,
    `X(z) = a \\cdot X_1(z) + b\\cdot X_2(z) \\tag{1.12}`,
    `Z[x(nT-mT)] = z^{-m} \\cdot X(z) \\tag{1.13}`,
    `y(nT) = \\sum_{m=0}^{\\infty}x_{1}(mT) \\cdot x_{2}(nT-mT) \\tag{1.14}`,
    `y(nT) = \\sum_{m=0}^{\\infty}x_{2}(mT) \\cdot x_{1}(nT-mT) \\tag{1.15}`,
    `Y(z) = X_{1}(z) \\cdot X_{2}(z) \\tag{1.16}`,
    `X(z) = \\sum_{k=0}^{\\infty}x(kT) \\cdot z^{-k} \\tag{1.17}`,
  ],
  e_2: [
    `X(k) = \\sum_{n=0}^{N-1}x(nT)\\cdot e^{(-2\\pi j\\cdot nk/N)} = \\sum_{n=0}^{N-1}x(nT)\\cdot W^{-nk} \\tag{2.1}`,
    `e^{j\\omega T} = cos(\\omega T) + j\\cdot sin(\\omega T) \\tag{2.2}`,
    `|X(kT)| = \\sqrt{Re(X)^2 + Im(X)^2} \\tag{2.3}`,
    `arg(X(kT)) = \\arctan{\\frac{Im(X)}{Re(X)}} \\tag{2.4}`,
    `x(nT) = \\frac{1}{N}\\sum_{n=0}^{N-1}X(k)\\cdot e^{(2\\pi j\\cdot nk/N)} = \\frac{1}{N}\\sum_{n=0}^{N-1}X(k)\\cdot W^{nk} \\tag{2.5}`,
    `y(nT) = e^{j\\psi nT} \\cdot x(nT) = \\cos{(\\psi nT)} \\cdot x(nT) + j \\sin{(\\psi nT)} \\cdot x(nT) \\tag{2.6}`,
    `Z[y(nT)] = Z[x(nT-mT)] = X(z)\\cdot z^{-m} \\tag{2.7}`,
    `Y(\\omega) = e^{-j\\omega mT} \\cdot X(e^{j\\omega T}) \\tag{2.8}`,
    `X'(k) = X(k) \\cdot e^{- \\frac{2\\pi j}{N} k m} \\tag{2.9}`,
    `X(k) = \\sum_{n=0}^{N-1}x(n)\\cdot \\cos{(2\\pi nk/N)} \\tag{2.10}`,
    `X(k) = \\sum_{n=0}^{N-1}x(n)\\cdot \\sin{(2\\pi nk/N)} \\tag{2.11}`,
    `x(n) = \\sum_{m=0}^{N-1}a(n)\\cdot b(n-m) \\tag{2.12}`,
    `X(k) = A(k) \\cdot B(k) \\tag{2.13}`,
    `X(k) = \\frac{1}{N} \\sum_{m=0}^{N-1}A(m)\\cdot B(k-m) \\tag{2.14}`,
    `x'(n) = x(n)\\cdot e^{\\frac{2\\pi j}{N} k m} \\tag{2.15}`,
    `\\sum_{n=0}^{N-1}x^{2}(n) = \\frac{1}{N} \\sum_{n=0}^{N-1} |{X^{2}(k)}| \\tag{2.16}`,
    `A_0 = \\sum_{n=0}^{NM}x(nT) \\tag{2.17}`,
    `W_{k,n} = e^{\\frac{-2\\pi j}{N}nk} \\tag{2.18}`,
    `X = A + B\\cdot W^{-k}_{N} \\\\
Y = A - B\\cdot W^{-k}_{N}
\\tag{2.19}`,
    `X = A + B \\\\
Y = (A - B)\\cdot W^{-k}_{N}
\\tag{2.20}`,
  ],
  e_3: [
    `r_{12} = \\frac{1}{N} \\sum_{n=0}^{N-1}x_1(n)x_2(n) \\tag{3.1}`,
    `\\Psi(\\tau) = Re[IFFT( | FFT(x) |^2 )] \\tag{3.2}`,
    `a*b = b*a \\tag{3.3}`,
    `\\sum_{m=0}^{N-1}a(m)b(n-m) = \\sum_{m=0}^{N-1}a(n-m)b(n) \\tag{3.4}`,
    `a*(b+c) = a*b + a*c \\tag{3.5}`,
    `a*(b*c) = (a*b)*c = (a*c)*b \\tag{3.6}`,
    `s(n) = a*b = \\sum_{m=0}^{n}a(m)\\cdot b(n-m) \\tag{3.7}`,
    `s(n) = a*b = \\sum_{m=0}^{N-1}a(m)\\cdot b(n-m) \\tag{3.8}`,
    `a(n) * b(n) = A(k) \\cdot B(k) \\tag{3.9}`,
  ],
  e_4: [
    `m = \\frac{1}{N}\\sum_{n=0}^{N-1}x(n) \\tag{4.1}`,
    `\\sigma^2 = \\frac{1}{N-1}\\sum_{n=0}^{N-1}|x(n) - m|^{2} \\tag{4.2}`,
    `f(x) = \\frac{1}{\\sigma \\sqrt{2\\pi}} e^{- \\frac{(x-\\mu)^2}{2\\sigma^2}} \\tag{4.3}`,
  ],
  e_5: [
    `s(t) = A \\cdot cos(2\\pi ft +\\phi) \\tag{5.1}`,
    `s = A \\cdot e^{j(2\\pi ft +\\phi)} \\tag{5.2}`,
    `s(t) = A_c \\cdot (1 + m \\cdot cos(\\omega_mt +\\phi)) \\cdot cos(\\omega_ct) \\tag{5.3}`,
    `A_m = \\frac{A_o\\cdot m}{2} \\tag{5.4}`,
    `s(t) = A \\cdot cos(2\\pi f_c t + k u_{m}(t)) \\tag{5.5}`,
    `s(t) =  A_c \\cdot cos(2\\pi f_c t + \\frac{A_{m} f_{\\Delta}}{f_{m}} sin(2\\pi f_s t)) \\tag{5.6}`,
    `f(t) = f_{0} + k t \\tag{5.7}`,
    `\\beta = \\Delta f \\cdot \\tau \\tag{5.8}`,
    `s(t) = A cos(2\\pi f_{0}t + \\pi \\beta t^{2}) \\tag{5.9}`,
  ],
  e_6: [
    `y(k) = \\frac{1}{a_0}\\cdot(\\sum_{k=0}^{N}b_{k}x(n-k) - \\sum_{k=0}^{M}a_{k}y(n-k)) \\tag{6.1}`,
    `H(z) = \\frac{B(z)}{A(z)} = \\frac{b_0 + b_{1}z^{-1} + ... + b_{N}z^{-N}}{1 + a_{1}z^{-1} + ... + a_{M}z^{-M}} \\tag{6.2}`,
    `y(n) = \\sum_{k=0}^{N-1}h(k)x(n-k) \\tag{6.3}`,
    `H(z) = \\sum_{k=0}^{N-1}h(k)z^{-k} \\tag{6.4}`,
    `y(n) = \\sum_{k=0}^{N}b_{k}x(n-k) - \\sum_{k=1}^{M}a_{k}y(n-k) \\tag{6.5}`,
    `y(n) = b_{0}x(n) + b_{1}x(n-1) + b_{M}x(n-M) + ... - a_{1}y(n-1) - a_{2}y(n-2) - ... - a_{N}y(n-N) \\tag{6.6}`,
    `H(z) = \\frac{B(z)}{A(z)} = \\frac{b_0 + b_{1}z^{-1} + ... + b_{N}z^{-N}}{1 + a_{1}z^{-1} + ... + a_{M}z^{-M}} \\tag{6.7}`,
    `H(z) = \\frac{b_0 + b_{1}z^{-1} + b_{2}z^{-2}}{1 + a_{1}z^{-1} + a_{2}z^{-2}} \\tag{6.8}`,
    `H(z) = 1 + z^{-1} + z^{-2} + z^{-3} \\tag{6.9}`,
    `\\frac{1 - z^{-1}}{(1 - z^{-1}} \\tag{6.10}`,
    `H(z) = \\frac{1 + z^-3}{1 - z^-1} \\tag{6.11}`,
    `\\tau_{\\phi} = - \\theta(\\omega)/\\omega \\tag{6.12}`,
    `\\tau_{g} = - d\\theta(\\omega)/\\omega \\tag{6.13}`,
  ],
  e_7: [
    `\\frac{sin(x)}{x} = sinc(x) \\tag{7.1}`,
    `\\beta = \\frac{A_{w}}{A_{r}} \\cdot \\frac{1}{N} \\sum_{n=0}^{N-1}w(n) \\tag{7.2}`,
    `w(n) = 1 \\tag{7.3}`,
    `w(n) = 1 - \\frac{n - N / 2}{L / 2} \\tag{7.4}`,
    `w(n) = sin(\\frac{\\pi\\cdot n}{N-1}) \\tag{7.5}`,
    `w(n) = 0.5 \\cdot [1 - cos(\\frac{2\\pi n}{N-1})] \\tag{7.6}`,
    `w(n) = 0.53836 - 0.46164 \\cdot cos(\\frac{2\\pi n}{N-1}) \\tag{7.7}`,
    `w(n) = a_0 - a_1 \\cdot cos(\\frac{2\\pi n}{N-1}) +  a_2 \\cdot cos(\\frac{4\\pi n}{N-1}) \\tag{7.8}`,
    `w(n) = a_0 - a_1 \\cdot cos(\\frac{2\\pi n}{N-1}) +  a_2 \\cdot cos(\\frac{4\\pi n}{N-1}) - a_3 \\cdot cos(\\frac{6\\pi n}{N-1}) \\tag{7.9}`,
    `w(n) = a_0 - a_1 \\cdot cos(\\frac{2\\pi n}{N-1}) +  a_2 \\cdot cos(\\frac{4\\pi n}{N-1}) - a_3 \\cdot cos(\\frac{6\\pi n}{N-1}) \\tag{7.10}`,
    `w(n) = a_0 - a_1 \\cdot cos(\\frac{2\\pi n}{N-1}) +  a_2 \\cdot cos(\\frac{4\\pi n}{N-1}) - a_3 \\cdot cos(\\frac{6\\pi n}{N-1}) + a_4 \\cdot cos(\\frac{8\\pi n}{N-1}) \\tag{7.11}`,
    `w(n) = \\frac{|I_{0} \\sqrt{1 - (\\frac{2n-N+1}{N-1})^2} |}{|I_{0}(\\beta)|} \\tag{7.12}`,
    `w(n) = e^{-\\frac{1}{2}(\\frac{n}{\\sigma})^{2}} \\tag{7.13}`,
  ],
  e_8: [
    `y[n] = x[nR] \\tag{8.1}`,
    `y[n] = \\sum_{k=0}^{N-1}x[nR-k]\\cdot h[k] \\tag{8.2}`,
    `y[n] = 
 \\begin{cases}
   x[n/R] , n = 0, L, 2L, ...\\\\
   0 , n \\ne 0, L, 2L, ...
 \\end{cases}
\\tag{8.3}`,
    `y[n] = \\sum_{k=0}^{N-1}x[nR-k]\\cdot h[k] \\tag{8.4}`,
    `H(z) = (\\sum_{k=0}^{RM-1}z^{-k})^N = [\\frac{1-z^{-RM}}{1-z^{-1}}]^{N} \\tag{8.5}`,
    `\\| H(f) \\| = [\\frac{sin(\\pi RMf)}{sin(\\pi f)}]^{N} \\tag{8.6}`,
    `B_{OUT} = ceil[Nlog_{2}(RM)+B_{IN}] \\tag{8.7}`,
    `B_{OUT} = ceil[log_{2}(\\frac{(RM)^{N}}{R})+B_{IN}] \\tag{8.8}`,
    `\\sum_{k=0}^{N-1}b_{k} = 1 \\tag{8.9}`,
    `H(z) = 1 + z^{-1} + z^{-2} + z^{-3} \\tag{8.10}`,
    `\\frac{1 - z^{-1}}{1 - z^{-1}} \\tag{8.11}`,
    `H(z) = \\frac{1 + z^-3}{1 - z^-1} \\tag{8.12}`,
  ],
  e_9: [
    `P_x(e^{j\\omega}) = \\sum_{-\\infty}^{\\infty} r_x(k)e^{jk\\omega} \\tag{9.1}`,
    `\\hat{r}(k) = \\frac{1}{N}\\sum_{n=0}^{N-1}x(n+k)x^*(n) \\tag{9.2}`,
    `x_N(n) =
 \\begin{cases}
   x(n), &0 \\leq n \\leq N \\\\
   0, &\\text{otherwise}
 \\end{cases}
 \\tag{9.3}`,
    `x_N(n) = \\omega_R(n)x(n) \\tag{9.4}`,
    `\\hat{r}(k) = \\frac{1}{N}x_N(k)*x_N(-k) \\tag{9.5}`,
    `\\hat{P}_{Per}(e^{j\\omega}) = \\frac{1}{N}X_N(e^{j\\omega})X_N^*(e^{j\\omega}) = \\frac{1}{N} \\left| X_N(e^{j\\omega}) \\right|^2 \\tag{9.6}`,
    `\\hat{P}_{M}(e^{j\\omega}) = \\frac{1}{NU}\\left| \\sum_{n=-\\infty}^{\\infty} x(n)\\omega(n)e^{-jn\\omega}\\right|^2 \\tag{9.7}`,
    `U= \\frac{1}{N}\\sum_{n=0}^{N-1}|\\omega(n)|^2 \\tag{9.8}`,
    `\\lim_{N \\to \\infty} E\\{ \\hat{P}_{Per}(e^{j\\omega})\\} = P_x(e^{j\\omega}) \\tag{9.9}`,
    `\\hat{P}_B(e^{j\\omega}) = \\frac{1}{N} \\sum_{i=0}^{K-1}\\left| \\sum_{n=0}^{L-1} x(n+iL)e^{-jn\\omega} \\right|^2 \\tag{9.10}`,
  ],
  e_10: [
    `s(t) = A \\cdot cos(\\frac{2\\pi\\cdot t}{T}) \\tag{10.1}`,
    `f(x) = \\frac{1}{\\sigma \\sqrt{2\\pi}} e^{- \\frac{(x-\\mu)^2}{2\\sigma^2}} \\tag{10.2}`,
    `s(t) = A \\cdot cos(\\frac{2\\pi\\cdot t}{T}) + N(\\mu,\\sigma) \\tag{10.3}`,
  ],
  e_11: [
    `y[n] = x[n]*h[n]= \\sum_{k=0}^n x[n-k]h[k] \\tag{11.1}`,
    `Y(\\omega) = X(\\omega)\\cdot H(\\omega) \\tag{11.2}`,
    `\\underset{1\\times N} {\\mathbf{Y}(z)} = \\underset{1\\times N}{\\mathbf{X}(z)} \\cdot \\underset{N\\times N}{\\mathbf{H}(z)} \\tag{11.3}`,
    `\\mathbf{X}(z) = \\left[ X_0(z), X_1(z) ... X_{N-1}(z) \\right] \\tag{11.4}`,
    `X_i(z) = \\sum_{m=0}^{N-1} x(mN + i) z^{-m}, \\quad i=0,1,.. N-1 \\tag{11.5}`,
    `\\mathbf{H}(z) = \\left[ \\mathbf{H}_0^T(z), \\mathbf{H}_1^T(z) ... \\mathbf{H}_{N-1}^T(z) \\right] = \\begin{bmatrix}
H_{(N-1), 0} & H_{(N-1), 1} & ... & H_{(N-1), (N-1)}\\\\
H_{(N-2), 0} & H_{(N-2), 1} & ... & H_{(N-2), (N-1)} \\\\
. & . & & . \\\\
. & . & & .\\\\
. & . & & . \\\\
H_{0, 0} & H_{0, 1} & ... & H_{0, (N-1)}
\\end{bmatrix} \\tag{11.6}`,
    `H_{n,k}(z) = \\sum_{m=0}^{N-1} h_k(mN + n) z^{-m}  \\tag{11.7}`,
    `\\hat{x}[n] = x[n-d] \\tag{11.8}`,
    `\\mathbf{\\hat{X}}(z) = \\mathbf{Y}(z) \\cdot \\mathbf{G}(z) = \\mathbf{X}(z) \\cdot \\mathbf{H}(z) \\cdot \\mathbf{G}(z) \\tag{11.9}`,
    `\\mathbf{A^{-1}}(z) = \\mathbf{A}^H(z^{-1}) \\tag{11.10}`,
  ],
  e_12: [
    `t_k = k \\frac{M}{L F_s} - \\frac{d}{F_s} \\tag{12.1}`,
    `x_k = t_k F_s = k \\frac{M}{L} - d \\tag{12.2}`,
    `\\begin{equation*} 
\\begin{cases} 
a_0 + a_1 \\cdot 0 + a_2 \\cdot 0^2 + \\ldots + a_{N-1} \\cdot 0^{N-1} = s(0), \\\\ a_0 + a_1 \\cdot 1 + a_2 \\cdot 1^2 + \\ldots + a_{N-1} \\cdot 1^{N-1} = s(1), \\\\ a_0 + a_1 \\cdot 2 + a_2 \\cdot 2^2 + \\ldots + a_{N-1} \\cdot 2^{N-1} = s(2), \\\\ \\vdots \\\\ a_0 + a_1 \\cdot (N-1) + a_2 \\cdot (N-1)^2 + \\ldots + a_{N-1} \\cdot (N-1)^{N-1} = s(N-1). 
\\end{cases} 
\\end{equation*} \\tag{12.3}`,
  ],
  e_13: [
    `b = 13arctg(\\frac{0.76f}{1000}) + 3.5arctg((\\frac{f}{7500})^2) \\tag{13.1}`,
    `m = 2595 \\cdot \\log{(1 + \\frac{f}{700})}\\tag{13.2}`,
    `f = 700\\cdot(10^{\\frac{m}{2595}} - 1) \\tag{13.3}`,
    `x_w(t) = x(t) \\cdot w(t) \\tag{13.4}`,
    `F(\\tau, \\omega) = \\int\\limits_{-\\infty}^\\infty f(\\tau) W(t - \\tau) e^{-j\\omega\\tau}\\mathrm{d\\tau} \\tag{13.5}`,
    `F(m, \\omega) = \\sum_{n=-\\infty}^{\\infty}x_n w_{n - m}e^{-j\\omega n} \\tag{13.6}`,
    `E(\\tau, \\omega) = |F(\\tau, \\omega)|^2 \\tag{13.7}`,
  ],
  e_14: [
    `С\\{x(t)\\} = |F^{-1}\\{log(|F\\{x(t)\\}|^2)\\}|^2 \\tag{14.1}`,
    `x(t) = s(t) * h(t) \\tag{14.2}`,
    `X(\\omega) = S(\\omega) \\cdot H(\\omega) \\tag{14.3}`,
    `\\log X(\\omega) = \\log S(\\omega) + \\log H(\\omega) \\tag{14.4}`,
    `X(\\bar{\\omega}) = S(\\bar{\\omega}) + H(\\bar{\\omega}) \\tag{14.5}`,
    `l(n)
= 
\\begin{cases}
1,  n < \\tau\\\\
-1,  n \\ge \\tau\\\\
\\end{cases}
\\tag{14.6}`,
    `X_k = \\frac{1}{2}(x_0 + (-1)^kx_{N-1}) + \\sum_{n=1}^{N-2}x_n cos\\left[\\frac{\\pi n k}{N - 1}\\right], \\ k = 0, ..., N - 1\\tag{14.7}`,
    `\\Delta_t = c_t - c_{t-1} \\\\
\\Delta\\Delta_t = \\Delta_t - \\Delta_{t - 1} \\tag{14.8}`,
  ],
  e_15: [
    `y(t) \\to T(t, f) \\tag{15.1}`,
    `\\int\\limits_{-\\infty}^\\infty \\Phi(t)\\mathrm{dt} = 0 \\tag{15.2}`,
    `\\int\\limits_{-\\infty}^\\infty |\\Phi(t)|^2\\mathrm{dt} < \\infty  \\tag{15.3}`,
    `\\Phi_{a}(t) = (\\frac{2}{\\pi a^2})^{\\frac{1}{4}}e(-\\frac{t^2}{a^2} -jt) \\text{,} a = \\frac{\\omega}{\\sigma}  \\tag{15.4}`,
    `\\Phi(t)
= 
\\begin{cases}
1, \\text{ if } t \\in [0; \\frac{1}{2})\\\\
-1, \\text{ if } t \\in [\\frac{1}{2}; 1)\\\\
0, \\text{ if } t \\notin [0; 1)
\\end{cases}
\\tag{15.5}`,
    `\\Phi(t) = \\frac{2}{\\sqrt{3\\sigma}\\pi^\\frac{1}{4}}\\left[1 - (\\frac{t}{\\sigma})^2\\right]e^{-\\frac{t^2}{2\\sigma^2}} \\tag{15.6}`,
    `\\Phi(t) = ke^{j\\omega_{0}t} e^{-\\frac{t^2}{2}} \\tag{15.7}`,
    `\\Phi_{a}(t) = \\Phi(\\frac{t}{a}) \\tag{15.8}`,
    `\\Phi_{b}(t) = \\Phi(t - b) \\tag{15.9}`,
    `\\Phi_{a, b}(t) = \\Phi(\\frac{t - b}{a}) \\tag{15.10}`,
    `T(a, b) = \\frac{1}{\\sqrt{a}}\\int\\limits_{-\\infty}^\\infty y(t)\\hat{\\Phi}_{a, b}(t)\\mathrm{dt} = \\frac{1}{\\sqrt{a}}\\int\\limits_{-\\infty}^\\infty y(t)\\hat{\\Phi}(\\frac{t - b}{a})\\mathrm{dt} \\tag{15.11}`,
    `T(a, b) = \\int\\limits_{-\\infty}^\\infty y(t)\\Phi_{a, b}(t)\\mathrm{dt} = \\int\\limits_{-\\infty}^\\infty y(t)\\Phi(\\frac{t - b}{a})\\mathrm{dt} \\tag{15.12}`,
  ],
  e_16: [
    `S(k) = \\sum_{n=0}^{N-1}s(n)W_{N}^{nk} \\tag{16.1}`,
    `W_{N}^{-kN} = exp(-j\\frac{2\\pi}{N}Nn);  k = 0 ... N - 1 \\tag{16.2}`,
    `S(k) = W_{N}^{-kN}\\sum_{n=0}^{N-1}s(n)W_{N}^{nk} = \\sum_{n=0}^{N-1}s(n)W_{N}^{k(n - N)} \\tag{16.3}`,
    `S(k)=s(0)W_{N}^{-kN}+s(1)W_{N}^{-k(N-1)}+s(2)W_{N}^{-k(N-2)}+...+s(1)W_{N}^{-k} \\tag{16.4}`,
    `y_{N-1}(k)=W_N^{-k}(s(N-1) + y_{N-2}(k)) \\tag{16.5}`,
    `y_{r}(k)=W_N^{-k}(s(r) + y_{r-1}(k)) \\tag{16.6}`,
    `Y(z)=W_{N}^{-k}(X(z) + z^{-1}Y(z)) \\tag{16.7}`,
    `H(z) = \\frac{Y(z)}{X(z)}=\\frac{W_{N}^{-k}}{1-W^{-k}_{N}z^{-1}} \\tag{16.8}`,
    `H(z) = \\frac{W_{N}^{-k}}{1-W^{-k}_{N}z^{-1}} = \\frac{W_{N}^{-k}(1-W^{-k}_{N}z^{-1})}{1-W^{-k}_{N}z^{-1}(1-W^{-k}_{N}z^{-1})} = \\frac{W^{-k}_{N} - z^{-1}}{1 - z^{-1}(W^{-k}_{N} + W^{k}_{N}) + z^{-2}} \\tag{16.9}`,
    `W^{-k}_{N} + W^{k}_{N} = exp(j\\frac{2\\pi}{N}k) + exp(-j\\frac{2\\pi}{N}k) = 2cos(\\frac{2\\pi}{N}k) \\tag{16.10}`,
    `H(z)=\\frac{W^{-k}_{N} - z^{-1}}{1 - 2cos(\\frac{2\\pi}{N}k)z^{-1} + z^{-2}} \\tag{16.11}`,
    `S(k)=y_{N-1}(k)=W_N^{-k}v(N-1) - v(N-2) \\tag{16.12}`,
    `v(r) = s(r) + 2cos(\\frac{2\\pi}{N})v(r-1) - v(r-2) \\tag{16.13}`,
    `\\alpha=2cos(2\\pi\\frac{1}{8})=1.4142 \\\\
W^{-k}_{N}=W_{8}^{-1}=exp(j\\frac{2\\pi}{8})=0.7071 + 0.7071j \\tag{16.14}`,
  ],
};
